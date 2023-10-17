namespace EventsOutOfProcessByMessageBrokerShared
{
    public interface IEventBus
    {
        void Publish(IEvent @event);
    }
}