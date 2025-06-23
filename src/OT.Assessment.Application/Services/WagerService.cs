using AutoMapper;
using OT.Assessment.Application.Common;
using OT.Assessment.Application.Interfaces.Common.Messaging;
using OT.Assessment.Application.Interfaces.Services;
using OT.Assessment.Application.Models.DTOs.Wager;
using OT.Assessment.Domain.Entities;
using OT.Assessment.Domain.Entities.AuditTrail;

namespace OT.Assessment.Application.Services
{
    public class WagerService : IWagerService
    {
        private readonly IRabbitMqPublisher _publisher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public WagerService(IRabbitMqPublisher publisher,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _publisher = publisher;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> CreateWager(WagerDto dto)
        {
            var message = _mapper.Map<WagerMessage>(dto);
            await _publisher.PublishMessage(message);
            return Result.Success();
        }
        public async Task<Result> ProcessWager(WagerDto message)
        {
            var account = await _unitOfWork.Accounts.GetAccountById(message.AccountId);

            if (account == null)
                return Result.Failure("Account not found.");

            if (account.Balance < message.Amount)
                return Result.Failure("Insufficient funds.");

            account.Balance -= message.Amount;

            await _unitOfWork.Accounts.AccountTransaction(account);

            await _unitOfWork.Transactions.AddRecord(new Transaction
            {
                EntityId = account.Id,
                EntityType = nameof(Account),
                Action = "Debited",
                Metadata = $"Wager placed for {message.Amount} in game: {message.GameId}"
            });

            var wager = new Wager
            {
                Id = Guid.NewGuid(),
                AccountId = message.AccountId,
                GameId = message.GameId,
                Amount = message.Amount,
                CreatedAt = message.CreatedAt
            };

            var response = await _unitOfWork.Wagers.AddWager(wager);

            return Result.Success();
        }

    }
}

