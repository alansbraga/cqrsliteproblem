using CQRSlite.Events;
using System;

namespace TestTaks.Events
{
    public class NewAggCreated : IEvent
    {
        public NewAggCreated(Guid id, string description)
        {
            Id = id;
            Description = description;
        }

        public Guid Id { get; set; }
        public string Description { get; set;  }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}