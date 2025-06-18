using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OT.Assessment.Application.Interfaces.Services;
using OT.Assessment.Application.Models.DTOs.Game;
using OT.Assessment.Domain.Entities;
using OT.Assessment.Domain.Interfaces.Repositories;

namespace OT.Assessment.Application.Services
{
    public class GameService : IGameService
    {
        private readonly ILogger<GameService> _logger;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        public GameService(IGameRepository gameRepository, IMapper mapper, ILogger<GameService> logger)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<Guid>> CreateGame(GameDto dto)
        {
            try
            {
                var game = _mapper.Map<Game>(dto);
                var result = await _gameRepository.AddNewGame(game);

                if (!result)
                {
                    return Result.Fail("Failed to create new game.");
                }

                return Result.Ok(game.Id);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, $"Failed to create game({dto.GameName}). Error: {dbEx.Message}");
                return Result.Fail<Guid>($"A database error occurred when creating game: {dto.GameName}.");
            }
        }

        public Task<Guid> EditGame(CreateGameDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<CreateGameDto> GetGame(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CreateGameDto>> GetGames()
        {
            throw new NotImplementedException();
        }
    }
}
