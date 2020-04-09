using CQRSlite.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestTaks.Commands
{
    public class CreateNewAgg: IRequest, ICommand
    {
        public CreateNewAgg(Guid id, string description, bool createTwice)
        {
            Id = id;
            Description = description;
            CreateTwice = createTwice;
        }

        public Guid Id { get; }
        public string Description { get; }
        public bool CreateTwice { get; }
    }
}
