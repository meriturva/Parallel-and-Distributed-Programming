
namespace EventsOutOfProcessByMessageBrokerShared
{
    public class NewOrderEvent: IEvent
    {
        public DateTime Created { get; set; } = DateTime.Now;
        public string UserEmail { get; set; } = default!;
    }
}
