using Microsoft.EntityFrameworkCore;
using OT.Assessment.Domain.Entities;

namespace OT.Assessment.Infrastructure.Persistance
{
    public class OTAssessmentDbContext : DbContext
    {
        public OTAssessmentDbContext(DbContextOptions<OTAssessmentDbContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; } = default!;
        public DbSet<Wager> Wagers { get; set; } = default!;
        public DbSet<Player> Players { get; set; } = default!;
        public DbSet<Game> Games { get; set; } = default!;
        public DbSet<GameTransaction> GameTransactions { get; set; } = default!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=localhost;Database=OT_Assessment_DB;Integrated Security=SSPI;");
        //    }

        //    base.OnConfiguring(optionsBuilder);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OTAssessmentDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
