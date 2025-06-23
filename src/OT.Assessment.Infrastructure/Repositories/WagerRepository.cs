using OT.Assessment.Domain.Entities;
using OT.Assessment.Domain.Interfaces.Repositories;
using OT.Assessment.Infrastructure.Persistance;

namespace OT.Assessment.Infrastructure.Repositories
{
    public class WagerRepository : IWagerRepository
    {
        private readonly OTAssessmentDbContext _context;
        public WagerRepository(OTAssessmentDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddWager(Wager wager)
        {
            await _context.Wagers.AddAsync(wager);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
    }
}
