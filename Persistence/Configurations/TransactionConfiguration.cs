using Exam2C2P.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Exam2C2P.Persistence.Configurations
{
    public class TransactionConfiguration :  IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {

            builder.Property(e => e.Id);

            builder.Property(e => e.TransactionId).HasMaxLength(50);
            builder.Property(e => e.Amount);
            builder.Property(e => e.CurrencyCode);
            builder.Property(e => e.TransactionDate);
            builder.Property(e => e.Status);
            builder.Property(e => e.FileType);
        }
    }
}
