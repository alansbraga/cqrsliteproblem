using CQRSlite.Events;
using System;

namespace TestTaks.Events
{
    public class ItemAdded : IEvent
    {
        public ItemAdded(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}