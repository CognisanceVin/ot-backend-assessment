using AutoMapper;
using Microsoft.Extensions.Logging;
using OT.Assessment.Application.Common;
using OT.Assessment.Application.Interfaces.Services;
using OT.Assessment.Application.Models.DTOs.Account;
using OT.Assessment.Domain.Entities;
using OT.Assessment.Domain.Entities.AuditTrail;
using OT.Assessment.Domain.Interfaces.Repositories;

namespace OT.Assessment.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public AccountService(ILogger<AccountService> logger, IUnitOfWork unitOfWork, IMapper mapper, IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<Result> DepositToPlayerAccount(DepositToAccountDto depositDto)
        {

            if (depositDto.Amount <= 0)
                return Result.Failure("Deposit amount must be greater than zero.");

            var account = await _accountRepository.GetAccountByPlayerId(depositDto.PlayerId);

            if (account == null)
                return Result.Failure("Account for player not found.");

            account.Balance += depositDto.Amount;

            var result = await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _accountRepository.AccountTransaction(account);

                await _transactionRepository.AddRecord(new TransactionRecord
                {
                    Id = Guid.NewGuid(),
                    EntityId = account.Id,
                    EntityType = nameof(Account),
                    Action = "Deposit",
                    Metadata = $"Deposited {depositDto.Amount} for account: {depositDto.PlayerId}"
                });
            });

            if (result.IsFailure)
            {
                _logger.LogError(result.Error, "Transaction failed when adding a player game.");
                return Result.Failure(result.Error!);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occured while attempting to deposit into account.");
                return Result.Failure($"An error occured while processing deposit.");
            }
        }

    }
}
