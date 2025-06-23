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

        public Task<Account> GetAccountByUserId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Account>> GetAllAccounts()
        {
            throw new NotImplementedException();
        }
    }
}
