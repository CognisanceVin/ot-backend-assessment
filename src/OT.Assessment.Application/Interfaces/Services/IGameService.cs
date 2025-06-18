using FluentResults;
using OT.Assessment.Application.Models.DTOs.Game;

namespace OT.Assessment.Application.Interfaces.Services
{
    public interface IGameService
    {
        Task<Result<Guid>> CreateGame(GameDto dto);
        Task<Guid> EditGame(CreateGameDto dto);
        Task<IEnumerable<CreateGameDto>> GetGames();
        Task<CreateGameDto> GetGame(Guid Id);
    }
}
