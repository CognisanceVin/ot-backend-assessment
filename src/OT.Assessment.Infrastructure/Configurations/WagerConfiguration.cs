using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OT.Assessment.Domain.Entities;

namespace OT.Assessment.Infrastructure.Configurations
{
    public class WagerConfiguration : IEntityTypeConfiguration<Wager>
    {
        public void Configure(EntityTypeBuilder<Wager> builder)
        {
            builder.HasKey(w => w.WagerId);

            builder.Property(w => w.WagerId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(w => w.Theme).HasMaxLength(100);
            builder.Property(w => w.Provider).HasMaxLength(100);
            builder.Property(w => w.GameName).HasMaxLength(100);
            builder.Property(w => w.TransactionId).HasMaxLength(100);
            builder.Property(w => w.Username).HasMaxLength(100);
            builder.Property(w => w.CountryCode).HasMaxLength(10);

            builder.HasIndex(w => w.TransactionId);
            builder.HasIndex(w => w.AccountId);
            builder.HasIndex(w => w.CreatedDateTime);

            builder.HasOne(w => w.Account)
                .WithMany(a => a.Wagers)
                .HasForeignKey(w => w.AccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}