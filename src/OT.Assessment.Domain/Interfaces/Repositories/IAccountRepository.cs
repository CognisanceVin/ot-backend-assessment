using OT.Assessment.Domain.Entities;

namespace OT.Assessment.Domain.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task AddAccount(Account account);
        Task<IEnumerable<Account>> GetAllAccounts();
        Task<Account> GetAccountByPlayerId(Guid Id);
        Task<Account> GetAccountById(Guid Id);
        Task AccountTransaction(Account account);
        Task<int> GetNumberOfAccountsCreatedToday();
    }
}
