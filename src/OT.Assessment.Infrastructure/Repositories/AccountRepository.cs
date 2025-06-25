using Microsoft.EntityFrameworkCore;
using OT.Assessment.Domain.Entities;
using OT.Assessment.Domain.Interfaces.Repositories;
using OT.Assessment.Infrastructure.Persistance;

namespace OT.Assessment.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly OTAssessmentDbContext _context;
        public AccountRepository(OTAssessmentDbContext context)
        {
            _context = context;
        }

        public async Task AccountTransaction(Account account)
        {
            _context.Accounts.Update(account);
        }

        public async Task AddAccount(Account account)
        {
            await _context.Accounts.AddAsync(account);
        }

        public async Task<Account> GetAccountById(Guid id)
        {
            return await _context.Accounts.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Account> GetAccountByPlayerId(Guid id)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.PlayerId == id);
        }

        public Task<IEnumerable<Account>> GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetNumberOfAccountsCreatedToday()
        {
            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1).Date;

            return await _context.Accounts
                .Where(a => a.CreatedAt >= today && a.CreatedAt < tomorrow)
                .CountAsync();
        }
    }
}
