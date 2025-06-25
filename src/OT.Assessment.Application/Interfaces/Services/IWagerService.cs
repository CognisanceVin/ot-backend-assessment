using OT.Assessment.Application.Common;
using OT.Assessment.Application.Models.DTOs.Wager;

namespace OT.Assessment.Application.Interfaces.Services
{
    public interface IWagerService
    {
        Task<Result> CreateWager(WagerDto dto);
        Task<Result> ProcessWager(WagerDto dto);
        Task<Result<PaginatedList<WagerDto>>> GetPlayerWagers(Guid playerId, int page, int pageSize);
        Task<List<PlayerSpendingDto>> GetTopSpenders(int count);
    }
}
