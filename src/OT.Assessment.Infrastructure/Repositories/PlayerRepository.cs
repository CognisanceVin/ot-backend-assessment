using Microsoft.EntityFrameworkCore;
using OT.Assessment.Domain.Entities;
using OT.Assessment.Domain.Interfaces.Repositories;
using OT.Assessment.Infrastructure.Persistance;

namespace OT.Assessment.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly OTAssessmentDbContext _context;
        public PlayerRepository(OTAssessmentDbContext context)
        {
            _context = context;
        }


        public async Task AddNewPlayer(Player player)
        {
            await _context.Players.AddAsync(player);
        }

        public async Task<IEnumerable<Player>> GetAllPlayers()
        {
            return await _context.Players
                                    .Include(x => x.Account)
                                .ToListAsync();
        }

        public async Task<Player> GetPlayerByEmail(string email)
        {
            return await _context.Players
                                    .Include(x => x.Account)
                                .FirstOrDefaultAsync(i => i.EmailAddress == email);
        }

        public async Task<Player> GetPlayerByUsername(string username)
        {
            return await _context.Players
                                    .Include(x => x.Account)
                                .FirstOrDefaultAsync(i => i.Username == username);
        }

        public async Task<Player> GetPlayerById(Guid id)
        {
            return await _context.Players
                                    .Include(x => x.Account)
                                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task UpdatePlayer(Player player)
        {
            _context.Players.Update(player);
        }
    }
}
