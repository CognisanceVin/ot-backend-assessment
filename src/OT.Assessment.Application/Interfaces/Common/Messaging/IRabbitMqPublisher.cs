namespace OT.Assessment.Application.Interfaces.Common.Messaging
{
    public interface IRabbitMqPublisher
    {
        Task PublishMessage<T>(T message);
    }
}
