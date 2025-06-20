using Microsoft.EntityFrameworkCore;
using OT.Assessment.Domain.Entities;
using OT.Assessment.Domain.Interfaces.Repositories;
using OT.Assessment.Infrastructure.Persistance;

namespace OT.Assessment.Infrastructure.Repositories
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

        public async Task<IEnumerable<Game>> GetAllGames()
        {
            return await _context.Games.ToListAsync();
        }
    }
}
