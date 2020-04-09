using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestTaks.EF
{
    internal class EventStoredMapping : IEntityTypeConfiguration<EventStored>
    {
        public void Configure(EntityTypeBuilder<EventStored> builder)
        {
            builder.HasIndex(e => e.AggreggationId);
        }
    }
}
