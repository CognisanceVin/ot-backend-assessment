using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OT.Assessment.Application.Interfaces.Services;
using OT.Assessment.Application.Models.DTOs.Wager;
using OT.Assessment.Infrastructure.Messaging.RabbitMq.Configs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace OT.Assessment.Infrastructure.Messaging.RabbitMq.Consumers
{
    public class WagerConsumerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConnection _connection;
        private RabbitMQOptions _options;
        private readonly ILogger<WagerConsumerService> _logger;

        public WagerConsumerService(IServiceProvider serviceProvider, IConnection connection, IOptions<RabbitMQOptions> options, ILogger<WagerConsumerService> logger)
        {
            _serviceProvider = serviceProvider;
            _connection = connection;
            _options = options.Value;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var channel = await _connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(
                _options.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, args) =>
            {
                try
                {
                    var body = args.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);

                    var message = JsonSerializer.Deserialize<WagerMessage>(json);

                    if (message is null)
                    {
                        _logger.LogWarning("Received null or invalid message.");
                        await channel.BasicAckAsync(args.DeliveryTag, false);
                        return;
                    }

                    using var scope = _serviceProvider.CreateScope();
                    var wagerService = scope.ServiceProvider.GetRequiredService<IWagerService>();

                    var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

                    var wager = mapper.Map<WagerDto>(message);

                    var result = await wagerService.ProcessWager(wager);

                    if (result.IsSuccess)
                    {
                        await channel.BasicAckAsync(args.DeliveryTag, false);
                    }
                    else
                    {
                        _logger.LogError("Failed to process message: {Error}", result.Error);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Message failed: {ex.Message}");
                }
            };

            await channel.BasicConsumeAsync(
                queue: _options.QueueName,
                autoAck: false,
                consumer: consumer
            );
        }
    }
}
