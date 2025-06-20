using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OT.Assessment.Domain.Entities;

namespace OT.Assessment.Infrastructure.Persistance.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasOne(a => a.Player)
               .WithOne(p => p.Account)
               .HasForeignKey<Account>(a => a.PlayerId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}