using Exam2C2P.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Exam2C2P.Persistence.Configurations
{
    public class EventLogConfiguration :  IEntityTypeConfiguration<EventLog>
    {
        public void Configure(EntityTypeBuilder<EventLog> builder)
        {

            builder.Property(e => e.Id);

            builder.Property(e => e.EventId);
            builder.Property(e => e.LogLevel);
            builder.Property(e => e.Message);
            builder.Property(e => e.Created);
        }
    }
}
