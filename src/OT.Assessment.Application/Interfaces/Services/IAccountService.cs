using OT.Assessment.Application.Common;
using OT.Assessment.Application.Models.DTOs.Account;

namespace OT.Assessment.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<Result> DepositToPlayerAccount(DepositToAccountDto depositDto);
    }
}
