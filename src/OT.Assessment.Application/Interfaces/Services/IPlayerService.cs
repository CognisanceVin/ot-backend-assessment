
using OT.Assessment.Application.Common;
using OT.Assessment.Application.Models.DTOs.Player;

namespace OT.Assessment.Application.Interfaces.Services
{
    public interface IPlayerService
    {
        Task<Result<Guid>> CreatePlayer(PlayerDto dto);
        Task<Guid> EditPlayer(PlayerDto dto);
        Task<Result<IEnumerable<PlayerDto>>> GetPlayers();
        Task<Result<PlayerDto>> GetPlayer(Guid Id);
    }
}
