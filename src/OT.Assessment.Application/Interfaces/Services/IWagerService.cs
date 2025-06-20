using OT.Assessment.Application.Common;
using OT.Assessment.Application.Models.DTOs.Wager;

namespace OT.Assessment.Application.Interfaces.Services
{
    public interface IWagerService
    {
        Task<Result> CreateWager(WagerDto dto);
    }
}
