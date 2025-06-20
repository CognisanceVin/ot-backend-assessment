
using OT.Assessment.Application.Common;
using OT.Assessment.Application.Models.DTOs.Game;

namespace OT.Assessment.Application.Interfaces.Services
{
    public interface IGameService
    {
        Task<Result<GameDto>> CreateGame(GameDto dto);
        Task<Guid> EditGame(GameDto dto);
        Task<Result<IEnumerable<GameDto>>> GetGames();
        Task<GameDto> GetGame(Guid Id);
    }
}
