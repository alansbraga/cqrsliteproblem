using CQRSlite.Caching;
using CQRSlite.Domain;
using CQRSlite.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MediatR;
using TestTaks.EF;
using TestTaks.Commands;

namespace TestTaks
{
    class Program
    {
        static void Main(string[] args)
        {

            var services = new ServiceCollection();
            services.AddScoped<EventStoreEF>();
            services.AddScoped<IEventStore>(y => y.GetService<EventStoreEF>());
            services.AddSingleton<ICache, MemoryCache>();
            //services.AddScoped<IRepository>(y => new CacheRepository(new Repository(y.GetService<IEventStore>()), y.GetService<IEventStore>(), y.GetService<ICache>()));
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<ISession, Session>();
            //services.AddScoped<ISession>(y => y.GetService<ISessao>());
            var assemblyName = typeof(EventStoreEF).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<EventStoreEF>(options => options.UseSqlServer("Server=localhost;Database=MyDatabase;User ID=sa;Password=master;MultipleActiveResultSets=true;", x => x.MigrationsAssembly(assemblyName)), ServiceLifetime.Scoped);
            RegisterHandlers(services);

            var sp = services.BuildServiceProvider();

            var sender = sp.GetService<IMediator>();
            Console.Clear();
            Console.WriteLine("Choose an option: ");
            Console.WriteLine("1. Session.Add");
            Console.WriteLine("2. One Command Two Aggregates");
            var option = Console.ReadLine();


            try
            {
                switch (option)
                {
                    case "1":
                        SessionAdd(sender);
                        break;
                    case "2":
                        OneCommandTwoEvents(sender);
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine("Press Enter");
            Console.ReadLine();

        }

        private static void OneCommandTwoEvents(IMediator sender)
        {
            var id = Guid.NewGuid();
            sender.Send(new CreateNewAgg(id, "destiny", false)).ConfigureAwait(false).GetAwaiter().GetResult();
            var origin = Guid.NewGuid();
            sender.Send(new CreateNewAgg(origin, "origin", false)).ConfigureAwait(false).GetAwaiter().GetResult();
            sender.Send(new CopyItens(id, origin)).ConfigureAwait(false).GetAwaiter().GetResult(); 
        }

        private static void SessionAdd(IMediator sender)
        {
            sender.Send(new CreateNewAgg(Guid.NewGuid(), "tests", true)).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private static void RegisterHandlers(ServiceCollection services)
        {
            var carregados = Assembly.GetEntryAssembly().GetReferencedAssemblies().Select(Assembly.Load);
            var lista = new List<Assembly>
            {
                Assembly.GetEntryAssembly()
            };
            foreach (var item in carregados)
            {
                lista.Add(item);
            }
            services.AddMediatR(lista.ToArray());
        }
    }
}
