using CQRSlite.Events;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace TestTaks.EF
{
    class EventStoreEF: DbContext, IEventStore
    {
        protected DbSet<EventStored> Eventos { get; set; }

        public EventStoreEF(DbContextOptions options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EventStoredMapping());

        }

        public async Task<IEnumerable<IEvent>> Get(Guid aggregateId, int fromVersion, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                var retorno = new List<IEvent>();
                var eventos = Eventos
                    .Where(e => (e.AggreggationId == aggregateId) && (e.Version > fromVersion))
                    .OrderBy(e => e.Version)
                    .ToArray();
                foreach (var item in eventos)
                {
                    var evento = JsonConvert.DeserializeObject(item.Data, Type.GetType(item.EventClass));
                    retorno.Add((IEvent)evento);
                }
                return (IEnumerable<IEvent>)retorno;
            });
        }

        public async Task Save(IEnumerable<IEvent> events, CancellationToken cancellationToken = default)
        {
            foreach (var evento in events)
            {
                var eventoGuardado = new EventStored
                {
                    AggreggationId = evento.Id,
                    EventClass = evento.GetType().AssemblyQualifiedName,
                    Data = JsonConvert.SerializeObject(evento),
                    Id = Guid.NewGuid(),
                    TimeStamp = DateTime.Now,
                    User = "user",
                    Version = evento.Version
                };
                await Eventos.AddAsync(eventoGuardado);
                //await envioEventos.PublicarAsync((IEvento)evento);
            }
            await SaveChangesAsync();
        }

    }
}
