using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OT.Assessment.Application.Common;
using OT.Assessment.Application.Interfaces.Services;
using OT.Assessment.Application.Models.DTOs.Game;
using OT.Assessment.Domain.Entities;

namespace OT.Assessment.Application.Services
{
    public class GameService : IGameService
    {
        private readonly ILogger<GameService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GameService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GameService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<GameDto>> CreateGame(GameDto dto)
        {
            try
            {
                var game = _mapper.Map<Game>(dto);
                var result = await _unitOfWork.Games.AddNewGame(game);

                if (!result)
                {
                    return Result<GameDto>.Failure("Failed to create new game.");
                }

                return Result<GameDto>.Success(dto);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, $"Failed to create game({dto.Name}). Error: {dbEx.Message}");
                return Result<GameDto>.Failure($"A database error occurred when creating game: {dto.Name}.");
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
                return Result<IEnumerable<GameDto>>.Failure("Failed to retrieve games.");
            }
        }
    }
}
