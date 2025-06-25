using AutoMapper;
using Microsoft.Extensions.Logging;
using OT.Assessment.Application.Common;
using OT.Assessment.Application.Interfaces.Services;
using OT.Assessment.Application.Models.DTOs.Game;
using OT.Assessment.Domain.Entities;
using OT.Assessment.Domain.Entities.AuditTrail;
using OT.Assessment.Domain.Interfaces.Repositories;

namespace OT.Assessment.Application.Services
{
    public class GameService : IGameService
    {
        private readonly ILogger<GameService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGameRepository _gameRepository;
        private readonly ITransactionRepository _transactionRepository;
        public GameService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GameService> logger, IGameRepository gameRepository, ITransactionRepository transactionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _gameRepository = gameRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<Result<GameDto>> CreateGame(GameDto dto)
        {
            var game = _mapper.Map<Game>(dto);

            var result = await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _gameRepository.AddNewGame(game);

                await _transactionRepository.AddRecord(new TransactionRecord
                {
                    Id = Guid.NewGuid(),
                    EntityId = dto.Id,
                    EntityType = nameof(Game),
                    Action = "Create",
                    Metadata = $"Created a new game {dto.Name}"
                });
            });

            if (result.IsFailure)
            {
                _logger.LogError(result.Error, "Transaction failed when adding a new game.");
                return Result<GameDto>.Failure(result.Error!);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync();
                return Result<GameDto>.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occured while attempting to create a new game.");
                return Result<GameDto>.Failure($"An error occured while creating new game");
            }

        }

        public Task<Guid> EditGame(GameDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<GameDto> GetGame(Guid Id)
        {
            throw new NotImplementedException();
        }
        public async Task<Result<IEnumerable<GameDto>>> GetGames()
        {
            try
            {
                var games = await _unitOfWork.Games.GetAllGames();

                if (games is null)
                {
                    return Result<IEnumerable<GameDto>>.Failure("Failed to retrieve games.");
                }
                var mappedData = _mapper.Map<IEnumerable<GameDto>>(games);

                return Result<IEnumerable<GameDto>>.Success(mappedData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving games.");
                return Result<IEnumerable<GameDto>>.Failure("An error occurred while retrieving games.");
            }
        }
    }
}
