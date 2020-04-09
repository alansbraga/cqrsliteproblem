using CQRSlite.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestTaks.Commands
{
    public class CopyItens : IRequest, ICommand
    {
        public CopyItens(Guid id, Guid origin)
        {
            Id = id;
            Origin = origin;
        }

        public Guid Id { get; }
        public Guid Origin { get; }
    }
}
