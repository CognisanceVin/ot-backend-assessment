//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using OT.Assessment.Domain.Entities.AuditTrail;

//namespace OT.Assessment.Infrastructure.Persistance.Configurations
//{
//    internal class GameTransactionConfiguration : IEntityTypeConfiguration<GameTransaction>
//    {
//        public void Configure(EntityTypeBuilder<GameTransaction> builder)
//        {
//            builder.HasKey(t => t.Id);

//            builder.HasIndex(t => t.TransactionId).IsUnique();
//            builder.HasIndex(t => t.CreatedDateTime);
//            builder.HasIndex(t => t.AccountId);

//            builder.Property(t => t.TransactionTypeId).HasMaxLength(20);
//            builder.Property(t => t.Provider).HasMaxLength(100);
//        }
//    }
//}
