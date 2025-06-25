using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OT.Assessment.Domain.Entities;

namespace OT.Assessment.Infrastructure.Persistance.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

            builder.Property(a => a.AccountNumber)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(a => a.Balance)
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(a => a.Player)
                   .WithOne(p => p.Account)
                   .HasForeignKey<Account>(a => a.PlayerId)
                   .IsRequired();

            builder.HasIndex(a => a.AccountNumber)
                   .IsUnique();
        }
    }
}