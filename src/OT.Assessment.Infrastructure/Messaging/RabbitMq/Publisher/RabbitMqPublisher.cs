using Microsoft.Extensions.Options;
using OT.Assessment.Application.Interfaces.Common.Messaging;
using OT.Assessment.Infrastructure.Messaging.RabbitMq.Configs;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace OT.Assessment.Infrastructure.Messaging.RabbitMq.Publisher
{
    public class RabbitMqPublisher : IRabbitMqPublisher
    {
        private readonly ConnectionFactory _factory;
        private IConnection? _connection;
        private RabbitMQOptions _options;

        public RabbitMqPublisher(IConnection connection, IOptions<RabbitMQOptions> options)
        {
            _connection = connection;
            _options = options.Value;
        }

        public async Task PublishMessage<T>(T message)
        {
            using var channel = await _connection.CreateChannelAsync();

            var created = await channel.QueueDeclareAsync(
                queue: _options.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false
            );

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            await channel.BasicPublishAsync(
                 exchange: "",
                 routingKey: _options.QueueName,
                 body: body
             );
        }
    }
}
