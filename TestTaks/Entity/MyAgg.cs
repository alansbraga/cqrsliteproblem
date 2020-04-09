using CQRSlite.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using TestTaks.Events;

namespace TestTaks.Entity
{
    public class MyAgg : AggregateRoot
    {
        public MyAgg()
        {

        }

        public MyAgg(Guid id, string description)
        {
            Id = id;
            ApplyChange(new NewAggCreated(id, description));
        }
        public string Description { get; internal set; }

        public void Apply(NewAggCreated created)
        {
            Id = created.Id;
            Description = created.Description;
        }

        internal void CopyItens(MyAgg origin)
        {
            // Loop throug itens
            ApplyChange(new ItemAdded(Id));
            ApplyChange(new ItemAdded(Id));
        }

        public void Apply(ItemAdded itemAdded)
        {
            Console.WriteLine("Item Added");
        }
    }
}
