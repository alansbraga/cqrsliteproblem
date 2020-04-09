using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestTaks.EF
{
    class DevelopmentDatabase : IDesignTimeDbContextFactory<EventStoreEF>
    {
        public EventStoreEF CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<EventStoreEF>();

            var connectionString = "Server=localhost;Database=MyDatabase;User ID=sa;Password=master;MultipleActiveResultSets=true;";

            builder.UseSqlServer(connectionString);

            return new EventStoreEF(builder.Options);
        }

    }


}
