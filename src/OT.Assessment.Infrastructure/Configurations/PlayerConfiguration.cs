using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OT.Assessment.Domain.Entities;

namespace OT.Assessment.Infrastructure.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Username).HasMaxLength(100);
            builder.Property(p => p.Email).HasMaxLength(200);

            builder.HasIndex(p => p.Username).IsUnique();
        }
    }
}
