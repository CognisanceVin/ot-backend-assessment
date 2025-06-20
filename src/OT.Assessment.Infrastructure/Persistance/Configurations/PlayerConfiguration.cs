using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OT.Assessment.Domain.Entities;

namespace OT.Assessment.Infrastructure.Persistance.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Username).IsRequired();
            builder.Property(p => p.EmailAddress).IsRequired();
            builder.HasIndex(p => p.Username).IsUnique();
            builder.HasIndex(p => p.EmailAddress).IsUnique();
        }
    }
}
