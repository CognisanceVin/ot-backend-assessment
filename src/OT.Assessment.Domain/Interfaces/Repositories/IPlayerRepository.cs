using OT.Assessment.Domain.Entities;

namespace OT.Assessment.Domain.Interfaces.Repositories
{
    public interface IPlayerRepository
    {
        Task AddNewPlayer(Player gaplayerme);
        Task UpdatePlayer(Player player);
        Task<IEnumerable<Player>> GetAllPlayers();
        Task<Player> GetPlayerById(Guid id);
        Task<Player> GetPlayerByEmail(string email);
        Task<Player> GetPlayerByUsername(string username);
    }
}
