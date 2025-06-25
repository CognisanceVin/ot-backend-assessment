using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using OT.Assessment.Application;
using OT.Assessment.Application.Common;
using OT.Assessment.Application.Models.DTOs.Game;
using OT.Assessment.Application.Services;
using OT.Assessment.Domain.Entities.AuditTrail;
using OT.Assessment.Domain.Interfaces.Repositories;
using Xunit;
using Game = OT.Assessment.Domain.Entities.Game;

namespace OT.Assessment.Tester.Services
{
    public class GameServiceTests
    {
        private GameDto CreateFakeGameDto() =>
            new GameDto { Id = Guid.NewGuid(), Name = "FakeGame", GameCode = "FG001" };

        [Fact]
        public async Task CreateGame_ShouldReturnSuccess()
        {
            var dto = CreateFakeGameDto();
            var gameEntity = new Game { Name = dto.Name, GameCode = dto.GameCode };

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<Game>(dto)).Returns(gameEntity);

            var mockGameRepo = new Mock<IGameRepository>();
            mockGameRepo.Setup(r => r.AddNewGame(It.IsAny<Game>())).ReturnsAsync(true);

            var mockTransactionRepo = new Mock<ITransactionRepository>();
            mockTransactionRepo.Setup(r => r.AddRecord(It.IsAny<TransactionRecord>())).Returns(Task.CompletedTask);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.Games).Returns(mockGameRepo.Object);
            mockUnitOfWork.Setup(u => u.ExecuteInTransactionAsync(It.IsAny<Func<Task>>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(Result.Success());
            mockUnitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var mockLogger = new Mock<ILogger<GameService>>();

            var service = new GameService(mockUnitOfWork.Object, mockMapper.Object, mockLogger.Object,
                                          mockGameRepo.Object, mockTransactionRepo.Object);

            var result = await service.CreateGame(dto);

            Assert.True(result.IsSuccess);
            Assert.Equal(dto.Name, result.Value.Name);
            Assert.Equal(dto.GameCode, result.Value.GameCode);
        }

        [Fact]
        public async Task CreateGame_ShouldReturnFailure_WhenTransactionFails()
        {
            var dto = CreateFakeGameDto();
            var gameEntity = new Game { Name = dto.Name, GameCode = dto.GameCode };

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<Game>(dto)).Returns(gameEntity);

            var mockGameRepo = new Mock<IGameRepository>();
            mockGameRepo.Setup(r => r.AddNewGame(It.IsAny<Game>())).ReturnsAsync(true);

            var mockTransactionRepo = new Mock<ITransactionRepository>();
            mockTransactionRepo.Setup(r => r.AddRecord(It.IsAny<TransactionRecord>())).Returns(Task.CompletedTask);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.Games).Returns(mockGameRepo.Object);
            mockUnitOfWork.Setup(u => u.ExecuteInTransactionAsync(It.IsAny<Func<Task>>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(Result.Failure("Transaction failed"));
            mockUnitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

            var mockLogger = new Mock<ILogger<GameService>>();

            var service = new GameService(mockUnitOfWork.Object, mockMapper.Object, mockLogger.Object,
                                          mockGameRepo.Object, mockTransactionRepo.Object);

            var result = await service.CreateGame(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Transaction failed", result.Error);
        }

        [Fact]
        public async Task GetGames_ShouldReturnSuccess_WithGames()
        {
            var games = new List<Game>
            {
                new Game { Name = "Game1", GameCode = "G001" },
                new Game { Name = "Game2", GameCode = "G002" },
                new Game { Name = "Game3", GameCode = "G003" }
            };

            var mockGameRepo = new Mock<IGameRepository>();
            mockGameRepo.Setup(r => r.GetAllGames()).ReturnsAsync(games);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.Games).Returns(mockGameRepo.Object);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<IEnumerable<GameDto>>(games))
                      .Returns(new List<GameDto>
                      {
                          new GameDto { Name = "Game1", GameCode = "G001" },
                          new GameDto { Name = "Game2", GameCode = "G002" },
                          new GameDto { Name = "Game3", GameCode = "G003" }
                      });

            var mockLogger = new Mock<ILogger<GameService>>();

            var service = new GameService(mockUnitOfWork.Object, mockMapper.Object, mockLogger.Object,
                                          mockGameRepo.Object, Mock.Of<ITransactionRepository>());

            var result = await service.GetGames();

            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
        }

        [Fact]
        public async Task GetGames_ShouldReturnFailure_WhenExceptionThrown()
        {
            var mockGameRepo = new Mock<IGameRepository>();
            mockGameRepo.Setup(r => r.GetAllGames()).ThrowsAsync(new Exception("Database error"));

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.Games).Returns(mockGameRepo.Object);

            var mockMapper = new Mock<IMapper>();

            var mockLogger = new Mock<ILogger<GameService>>();

            var service = new GameService(mockUnitOfWork.Object, mockMapper.Object, mockLogger.Object,
                                          mockGameRepo.Object, Mock.Of<ITransactionRepository>());

            var result = await service.GetGames();

            Assert.False(result.IsSuccess);
            Assert.Equal("An error occurred while retrieving games.", result.Error);
        }
    }
}
