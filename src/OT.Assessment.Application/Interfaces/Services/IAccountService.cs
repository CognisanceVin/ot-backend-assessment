using OT.Assessment.Application.Common;
using OT.Assessment.Application.Models.DTOs.Wager;

namespace OT.Assessment.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<Result> GetPlayerAccount(Guid Id);
        Task<Result> ProcessWager(WagerMessage dto);
    }
}
