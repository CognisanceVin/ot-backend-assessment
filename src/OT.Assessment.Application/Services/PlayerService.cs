using AutoMapper;
using Microsoft.Extensions.Logging;
using OT.Assessment.Application.Common;
using OT.Assessment.Application.Interfaces.Common;
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
        private readonly IAccountNumberGenerator _accountNumberGenerator;
        public PlayerService(IMapper mapper,
            ILogger<PlayerService> logger,
            IPlayerRepository playerRepository,
            IAccountRepository accountRepository,
            ITransactionRepository transactionRepository,
            IAccountNumberGenerator accountNumberGenerator,
        IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _playerRepo = playerRepository;
            _accountRepo = accountRepository;
            _unitOfWork = unitOfWork;
            _transactionRepo = transactionRepository;
            _accountNumberGenerator = accountNumberGenerator;
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

                var accNo = await _accountNumberGenerator.GenerateAccountNumber();

                var result = await _unitOfWork.ExecuteInTransactionAsync(async () =>
                {
                    await _playerRepo.AddNewPlayer(player);

                    var account = new Account
                    {
                        PlayerId = player.Id,
                        Balance = 0,
                        AccountNumber = accNo
                    };

                    await _accountRepo.AddAccount(account);

                    await _transactionRepo.AddRecord(new TransactionRecord
                    {
                        EntityId = dto.Id,
                        EntityType = nameof(Player),
                        Action = "Created",
                        Metadata = $"Created a new player."
                    });

                    await _transactionRepo.AddRecord(new TransactionRecord
                    {
                        EntityId = account.Id,
                        EntityType = nameof(Account),
                        Action = "Created",
                        Metadata = $"Account created for player: {player.Id}."
                    });
                });

                if (result.IsFailure)
                {
                    _logger.LogError(result.Error, "Transaction failed when adding a player game.");
                    return Result<Guid>.Failure(result.Error!);
                }

                try
                {
                    await _unitOfWork.SaveChangesAsync();
                    return Result<Guid>.Success(player.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error has occured while attempting to deposit money into account.");
                    return Result<Guid>.Failure("An error occurred while processing the deposit.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occured while attempting to create a player.");
                return Result<Guid>.Failure("An error occurred while creating player.");
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
