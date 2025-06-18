using OT.Assessment.Domain.Entities;
using OT.Assessment.Domain.Interfaces.Repositories;

namespace OT.Assessment.Infrastructure.Persistance.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly OTAssessmentDbContext _context;
        public GameRepository(OTAssessmentDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddNewGame(Game game)
        {
            await _context.Games.AddAsync(game);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

    }
}
