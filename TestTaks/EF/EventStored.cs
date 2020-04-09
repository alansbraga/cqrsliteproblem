using System;

namespace TestTaks.EF
{
    public class EventStored
    {
        public Guid Id { get; set; }
        public Guid AggreggationId { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string Data { get; set; }
        public string User { get; set; }
        public string EventClass { get; set; }

    }
}