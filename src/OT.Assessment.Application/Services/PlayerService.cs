using AutoMapper;
using Microsoft.Extensions.Logging;
using OT.Assessment.Application.Common;
using OT.Assessment.Application.Interfaces.Services;
using OT.Assessment.Application.Models.DTOs.Player;
using OT.Assessment.Domain.Entities;
using OT.Assessment.Domain.Entities.AuditTrail;
using OT.Assessment.Domain.Interfaces.Repositories;

namespace OT.Assessment.Application.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly ILogger<PlayerService> _logger;
        private readonly IPlayerRepository _playerRepo;
        private readonly IAccountRepository _accountRepo;
        private readonly ITransactionRepository _transactionRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public PlayerService(IMapper mapper,
            ILogger<PlayerService> logger,
            IPlayerRepository playerRepository,
            IAccountRepository accountRepository,
            ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _playerRepo = playerRepository;
            _accountRepo = accountRepository;
            _unitOfWork = unitOfWork;
            _transactionRepo = transactionRepository;
        }
        public async Task<Result<Guid>> CreatePlayer(PlayerDto dto)
        {
            try
            {
                var playerExist = await _playerRepo.GetPlayerByEmail(dto.EmailAddress);
                if (playerExist is not null)
                {
                    return Result<Guid>.Failure("Email Address already exists.");
                }

                playerExist = await _playerRepo.GetPlayerByUsername(dto.Username);

                if (playerExist is not null)
                {
                    return Result<Guid>.Failure("Username already exists.");
                }


                var player = _mapper.Map<Player>(dto);



                var result = await _unitOfWork.ExecuteInTransactionAsync(async () =>
                {
                    await _playerRepo.AddNewPlayer(player);

                    var account = new Account
                    {
                        PlayerId = player.Id,
                        Balance = 0
                    };

                    await _accountRepo.AddAccount(account);

                    await _transactionRepo.AddRecord(new Transaction
                    {
                        EntityId = account.Id,
                        EntityType = nameof(Account),
                        Action = "Created",
                        Metadata = $"Account created for player: {player.Id}."
                    });
                });

                if (result.IsFailure)
                {
                    return Result<Guid>.Failure(result.Error!);
                }

                return Result<Guid>.Success(player.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create player. Error: {ex.Message}");
                return Result<Guid>.Failure($"A database error occurred when creating player.");
            }
        }

        public Task<Guid> EditPlayer(PlayerDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<PlayerDto>> GetPlayer(Guid Id)
        {
            try
            {
                var player = await _playerRepo.GetPlayerById(Id);
                if (player is null)
                {
                    return Result<PlayerDto>.Failure("Failed to retrieve player.");
                }

                var mappedData = _mapper.Map<PlayerDto>(player);

                return Result<PlayerDto>.Success(mappedData);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving player.");
                return Result<PlayerDto>.Failure("Failed to retrieve players.");
            }
        }

        public async Task<Result<IEnumerable<PlayerDto>>> GetPlayers()
        {
            try
            {
                var players = await _playerRepo.GetAllPlayers();

                if (players is null)
                {
                    return Result<IEnumerable<PlayerDto>>.Failure("Failed to retrieve players.");
                }
                var mappedData = _mapper.Map<IEnumerable<PlayerDto>>(players);

                return Result<IEnumerable<PlayerDto>>.Success(mappedData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving players.");
                return Result<IEnumerable<PlayerDto>>.Failure("Failed to retrieve players.");
            }
        }
    }
}
