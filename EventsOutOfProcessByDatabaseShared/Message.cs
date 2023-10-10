using System;

namespace EventsOutOfProcessByDatabaseShared
{
    public class Message
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Type { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTime OccurredOn { get; set; } = DateTime.Now;
        public DateTime? ProcessedOn { get; set; }
    }
}