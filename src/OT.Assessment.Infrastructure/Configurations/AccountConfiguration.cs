using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OT.Assessment.Domain.Entities;

namespace OT.Assessment.Infrastructure.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.BrandId).HasMaxLength(50);
            builder.Property(a => a.Username).HasMaxLength(100);
            builder.Property(a => a.CountryCode).HasMaxLength(10);

            builder.HasIndex(a => a.Username);
        }
    }
}