using AutoMapper;
using OT.Assessment.Application.Interfaces.Services;
using OT.Assessment.Application.Models.DTOs.Wager;
using OT.Assessment.Domain.Entities;
using OT.Assessment.Domain.Entities.AuditTrail;
using System.Text.Json;

namespace OT.Assessment.Application.Services
{
    public class WagerProcessingService : IWagerProcessingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public WagerProcessingService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task ProcessWager(WagerMessage message)
        {
            var wager = _mapper.Map<Wager>(message);
            var account = await _unitOfWork.Accounts.GetAccountById(message.AccountId);

            if (account == null || account.Balance < message.Amount)
                throw new Exception("Invalid account or insufficient balance");

            account.Balance -= message.Amount;

            await _unitOfWork.Wagers.AddWager(wager);

            await _unitOfWork.Transactions.AddRecord(new TransactionRecord
            {
                EntityId = wager.Id,
                EntityType = nameof(Wager),
                Action = "Created",
                Metadata = JsonSerializer.Serialize(wager)
            });

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
