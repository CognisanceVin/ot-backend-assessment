using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OT.Assessment.Infrastructure.Persistance
{
    public class OTAssessmentDbContextFactory : IDesignTimeDbContextFactory<OTAssessmentDbContext>
    {
        public OTAssessmentDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OTAssessmentDbContext>();

            // Replace this connection string with your actual one
            optionsBuilder.UseSqlServer("Server=localhost;Database=OT_Assessment_DB; Integrated Security=SSPI;TrustServerCertificate=True;");

            return new OTAssessmentDbContext(optionsBuilder.Options);
        }
    }
}
