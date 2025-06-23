using AutoMapper;
using Microsoft.Extensions.Logging;
using OT.Assessment.Application.Common;
using OT.Assessment.Application.Interfaces.Services;
using OT.Assessment.Application.Models.DTOs.Wager;

namespace OT.Assessment.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(ILogger<AccountService> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;

        }
        public Task<Result> GetPlayerAccount(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Result> ProcessWager(WagerMessage dto)
        {
            throw new NotImplementedException();
        }
    }
}
