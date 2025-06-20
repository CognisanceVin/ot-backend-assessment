using OT.Assessment.Domain.Entities;

namespace OT.Assessment.Domain.Interfaces.Repositories
{
    public interface IGameRepository
    {
        Task<bool> AddNewGame(Game game);
        Task<IEnumerable<Game>> GetAllGames();
    }
}
