using Microsoft.Extensions.Options;
using OT.Assessment.Infrastructure.Messaging.RabbitMq.Configs;
using RabbitMQ.Client;
using System.Text;

namespace OT.Assessment.Infrastructure.Messaging.RabbitMq
{
    public class RabbitMqPublisher
    {
        private readonly ConnectionFactory _factory;
        private IConnection? _connection;
        private RabbitMQOptions _options;

        public RabbitMqPublisher(IOptions<RabbitMQOptions> options)
        {

            _options = options.Value;
            _factory = new ConnectionFactory
            {
                HostName = _options.HostName,
                UserName = _options.UserName,
                Password = _options.Password,
                VirtualHost = _options.VirtualHost,
                Port = _options.Port,
            };
        }

        public async Task PublishAsync(string queueName, string message)
        {
            await using var connection = await _factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: queueName,
                                            durable: true,
                                            exclusive: false,
                                            autoDelete: false);

            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(exchange: "",
                                            routingKey: queueName,
                                            body: body);
        }
    }
}
