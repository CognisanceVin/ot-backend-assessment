using OT.Assessment.Application.Interfaces.Common;
using OT.Assessment.Domain.Interfaces.Repositories;

namespace OT.Assessment.Application.Helpers
{
    public class AccountNumberGenerator : IAccountNumberGenerator
    {
        private readonly IAccountRepository _accountRepository;
        public AccountNumberGenerator(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<string> GenerateAccountNumber()
        {
            var today = DateTime.UtcNow.ToString("ddMMyyyy");
            var countToday = await _accountRepository.GetNumberOfAccountsCreatedToday();
            var sequence = (countToday + 1).ToString("D4");

            return $"acc-{today}-{sequence}";
        }
    }
}
