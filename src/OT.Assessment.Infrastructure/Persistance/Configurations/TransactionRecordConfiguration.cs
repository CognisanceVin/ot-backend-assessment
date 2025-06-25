using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OT.Assessment.Domain.Entities.AuditTrail;

namespace OT.Assessment.Infrastructure.Persistence.Configurations
{
    public class TransactionRecordConfiguration : IEntityTypeConfiguration<TransactionRecord>
    {
        public void Configure(EntityTypeBuilder<TransactionRecord> builder)
        {
            builder.ToTable("TransactionRecords");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(t => t.EntityId)
                   .IsRequired();

            builder.Property(t => t.EntityType)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(t => t.Action)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(t => t.Metadata)
                   .HasColumnType("nvarchar(max)");

            builder.Property(t => t.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.HasIndex(t => new { t.EntityType, t.EntityId });
        }
    }
}
