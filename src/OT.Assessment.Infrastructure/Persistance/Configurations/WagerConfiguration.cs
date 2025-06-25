using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OT.Assessment.Domain.Entities;

namespace OT.Assessment.Infrastructure.Persistence.Configurations
{
    public class WagerConfiguration : IEntityTypeConfiguration<Wager>
    {
        public void Configure(EntityTypeBuilder<Wager> builder)
        {
            builder.ToTable("Wagers");

            builder.HasKey(w => w.Id);

            builder.Property(w => w.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(w => w.Amount)
                   .IsRequired();

            builder.Property(w => w.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(w => w.Account)
                   .WithMany()
                   .HasForeignKey(w => w.AccountId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(w => w.Game)
                   .WithMany()
                   .HasForeignKey(w => w.GameId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(w => w.Id);
            builder.HasIndex(w => w.AccountId);
            builder.HasIndex(w => w.GameId);
        }
    }
}
