using Microsoft.EntityFrameworkCore;
using OT.Assessment.Domain.Entities;
using OT.Assessment.Domain.Entities.AuditTrail;

namespace OT.Assessment.Infrastructure.Persistance
{
    public class OTAssessmentDbContext : DbContext
    {
        public OTAssessmentDbContext(DbContextOptions<OTAssessmentDbContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; } = default!;
        public DbSet<Wager> Wagers { get; set; } = default!;
        public DbSet<Player> Players { get; set; } = default!;
        public DbSet<Game> Games { get; set; } = default!;
        public DbSet<Transaction> TransactionRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OTAssessmentDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
