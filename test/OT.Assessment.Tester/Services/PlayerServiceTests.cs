using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using OT.Assessment.Application;
using OT.Assessment.Application.Common;
using OT.Assessment.Application.Helpers;
using OT.Assessment.Application.Models.DTOs.Player;
using OT.Assessment.Application.Services;
using OT.Assessment.Domain.Entities;
using OT.Assessment.Domain.Interfaces.Repositories;
using Xunit;
using Player = OT.Assessment.Domain.Entities.Player;

namespace OT.Assessment.Tester.Services
{
    public class PlayerServiceTests
    {
        [Fact]
        public async Task CreatePlayer_ShouldReturnSuccess_WhenPlayerDoesNotExist()
        {
            var dto = new PlayerDto
            {
                Id = Guid.NewGuid(),
                EmailAddress = "test@example.com",
                Username = "testuser"
            };

            var playerEntity = new Player { Id = dto.Id, EmailAddress = dto.EmailAddress, Username = dto.Username };

            var mockPlayerRepo = new Mock<IPlayerRepository>();
            mockPlayerRepo.Setup(r => r.GetPlayerByEmail(dto.EmailAddress)).ReturnsAsync((Player?)null);
            mockPlayerRepo.Setup(r => r.GetPlayerByUsername(dto.Username)).ReturnsAsync((Player?)null);
            mockPlayerRepo.Setup(r => r.AddNewPlayer(It.IsAny<Player>())).Returns(Task.CompletedTask);

            var mockAccountRepo = new Mock<IAccountRepository>();
            mockAccountRepo.Setup(r => r.AddAccount(It.IsAny<Account>())).Returns(Task.CompletedTask);

            var mockTransactionRepo = new Mock<ITransactionRepository>();
            mockTransactionRepo.Setup(r => r.AddRecord(It.IsAny<Domain.Entities.AuditTrail.TransactionRecord>())).Returns(Task.CompletedTask);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.Players).Returns(mockPlayerRepo.Object);
            mockUnitOfWork.SetupGet(u => u.Accounts).Returns(mockAccountRepo.Object);
            mockUnitOfWork.SetupGet(u => u.Transactions).Returns(mockTransactionRepo.Object);
            mockUnitOfWork.Setup(u => u.ExecuteInTransactionAsync(It.IsAny<Func<Task>>(), default))
                          .ReturnsAsync(Result.Success());

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<Player>(dto)).Returns(playerEntity);

            var accountNumberGenerator = new AccountNumberGenerator(mockAccountRepo.Object);

            var mockLogger = new Mock<ILogger<PlayerService>>();

            var service = new PlayerService(
                mockMapper.Object,
                mockLogger.Object,
                mockPlayerRepo.Object,
                mockAccountRepo.Object,
                mockTransactionRepo.Object,
                accountNumberGenerator,
                mockUnitOfWork.Object
            );

            var result = await service.CreatePlayer(dto);

            Assert.True(result.IsSuccess);
            Assert.Equal(dto.Id, result.Value);
        }

        [Fact]
        public async Task CreatePlayer_ShouldFail_WhenEmailExists()
        {
            var dto = new PlayerDto { EmailAddress = "exists@example.com" };

            var mockPlayerRepo = new Mock<IPlayerRepository>();
            mockPlayerRepo.Setup(r => r.GetPlayerByEmail(dto.EmailAddress)).ReturnsAsync(new Player());

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.Players).Returns(mockPlayerRepo.Object);

            var mockMapper = new Mock<IMapper>();
            var mockAccountRepo = new Mock<IAccountRepository>();
            var mockTransactionRepo = new Mock<ITransactionRepository>();
            var accountNumberGenerator = new AccountNumberGenerator(mockAccountRepo.Object);
            var mockLogger = new Mock<ILogger<PlayerService>>();

            var service = new PlayerService(
                mockMapper.Object,
                mockLogger.Object,
                mockPlayerRepo.Object,
                mockAccountRepo.Object,
                mockTransactionRepo.Object,
                accountNumberGenerator,
                mockUnitOfWork.Object
            );

            var result = await service.CreatePlayer(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Email Address already exists.", result.Error);
        }
    }
}

