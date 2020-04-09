using CQRSlite.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaks.Entity;

namespace TestTaks.Commands
{
    public class CommandManager : IRequestHandler<CreateNewAgg>,
        IRequestHandler<CopyItens>
    {
        private readonly ISession session;

        public CommandManager(ISession session)
        {
            this.session = session;
        }

        public async Task<Unit> Handle(CreateNewAgg request, CancellationToken cancellationToken)
        {
            var agg = new MyAgg(request.Id, request.Description);

            await session.Add(agg);

            if (request.CreateTwice)
            {
                var agg2 = new MyAgg(Guid.NewGuid(), request.Description);
                await session.Add(agg2);
            }

            await session.Commit();
            var ret = new Unit();
            return ret;

        }

        public async Task<Unit> Handle(CopyItens request, CancellationToken cancellationToken)
        {
            var origin = await session.Get<MyAgg>(request.Origin);
            var destiny = await session.Get<MyAgg>(request.Id);
            destiny.CopyItens(origin);

            await session.Commit();
            var ret = new Unit();
            return ret;
        }
    }
}
