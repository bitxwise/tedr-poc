namespace Risly.Cqrs.Kafka
{
    public interface IEventHandler
    {
        void Handle(Event @event);
    }
}