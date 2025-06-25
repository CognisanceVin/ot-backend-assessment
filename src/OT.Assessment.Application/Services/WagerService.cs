using AutoMapper;
using Microsoft.Extensions.Logging;
using OT.Assessment.Application.Common;
using OT.Assessment.Application.Interfaces.Common.Messaging;
using OT.Assessment.Application.Interfaces.Services;
using OT.Assessment.Application.Models.DTOs.Wager;
using OT.Assessment.Domain.Entities;
using OT.Assessment.Domain.Entities.AuditTrail;
using OT.Assessment.Domain.Interfaces.Repositories;

namespace OT.Assessment.Application.Services
{
    public class WagerService : IWagerService
    {
        private readonly IRabbitMqPublisher _publisher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWagerRepository _wagerRepository;
        private readonly ILogger<WagerService> _logger;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public WagerService(IRabbitMqPublisher publisher,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IWagerRepository wagerRepository,
            ILogger<WagerService> logger,
            ITransactionRepository transactionRepository,
            IAccountRepository accountRepository)
        {
            _publisher = publisher;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _wagerRepository = wagerRepository;
            _logger = logger;
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        public async Task<Result> CreateWager(WagerDto dto)
        {
            var message = _mapper.Map<WagerMessage>(dto);
            await _publisher.PublishMessage(message);

            return Result.Success();
        }

        public async Task<Result<PaginatedList<WagerDto>>> GetPlayerWagers(Guid playerId, int page, int pageSize)
        {
            var result = await _wagerRepository.GetPlayerWagers(playerId);
            var count = result.Count();

            var mapped = _mapper.Map<IEnumerable<WagerDto>>(result);

            var paginatedResult = new PaginatedList<WagerDto>(
                                         mapped.ToList(),
                                         count,
                                         page,
                                         pageSize);

            return Result<PaginatedList<WagerDto>>.Success(paginatedResult);
        }

        public async Task<List<PlayerSpendingDto>> GetTopSpenders(int count)
        {
            var result = await _wagerRepository.GetTopSpenders();

            var topSpenders = result.GroupBy(w => new
            {
                w.Account.Id,
                w.Account.Player.Username,
                w.Account.Player.EmailAddress
            })
                                        .Select(g => new PlayerSpendingDto
                                        {
                                            AccountId = g.Key.Id,
                                            Username = g.Key.Username,
                                            TotalSpent = g.Sum(w => w.Amount)
                                        })
                                        .OrderByDescending(x => x.TotalSpent)
                                        .Take(count)
                                        .ToList();

            return topSpenders;
        }

        public async Task<Result> ProcessWager(WagerDto message)
        {
            var account = await _accountRepository.GetAccountById(message.AccountId);

            if (account == null)
                return Result.Failure("Account not found.");

            if (account.Balance < message.Amount)
                return Result.Failure("Insufficient funds.");

            account.Balance -= message.Amount;
            var result = await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {

                await _accountRepository.AccountTransaction(account);

                var wager = new Wager
                {
                    Id = Guid.NewGuid(),
                    AccountId = message.AccountId,
                    GameId = message.GameId,
                    Amount = message.Amount,
                    CreatedAt = message.CreatedAt
                };

                await _wagerRepository.AddWager(wager);

                await _transactionRepository.AddRecord(new TransactionRecord
                {
                    Id = Guid.NewGuid(),
                    EntityId = account.Id,
                    EntityType = nameof(Wager),
                    Action = "Placed",
                    Metadata = $"Wager placed for {message.Amount} in game: {message.GameId}"
                });

                await _transactionRepository.AddRecord(new TransactionRecord
                {
                    Id = Guid.NewGuid(),
                    EntityId = account.Id,
                    EntityType = nameof(Account),
                    Action = "Debited",
                    Metadata = $"Wager placed for {message.Amount} in game: {message.GameId}"
                });
            });

            if (result.IsFailure)
            {
                _logger.LogError(result.Error, "Transaction failed when adding a wager.");
                return Result.Failure(result.Error!);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occured while attempting to deposit money into account.");
                return Result.Failure("An error occurred while processing the deposit.");
            }
        }
    }
}

