using OT.Assessment.Domain.Entities;

namespace OT.Assessment.Domain.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task AddAccount(Account account);
        Task<IEnumerable<Account>> GetAllAccounts();
        Task<Account> GetAccountByUserId(Guid Id);
        Task<Account> GetAccountById(Guid Id);
    }
}
