namespace OT.Assessment.Infrastructure.Messaging.RabbitMq.Configs
{
    public class RabbitMQOptions
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string QueueName { get; set; } = default!;
        public int Port { get; set; }
        public string VirtualHost { get; set; }
    }
}
