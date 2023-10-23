
using ProtoBuf;

namespace EventsOutOfProcessByMessageBrokerShared
{
    [ProtoContract]
    public class NewOrderEvent : IEvent
    {
        [ProtoMember(1)]
        public DateTime Created { get; set; } = DateTime.Now;
        [ProtoMember(2)]
        public string UserEmail { get; set; } = default!;
    }
}
