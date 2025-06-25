using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using OT.Assessment.Application;
using OT.Assessment.Application.Common;
using OT.Assessment.Application.Interfaces.Common.Messaging;
using OT.Assessment.Application.Models.DTOs.Wager;
using OT.Assessment.Application.Services;
using OT.Assessment.Domain.Entities;
using OT.Assessment.Domain.Interfaces.Repositories;
using Xunit;

namespace OT.Assessment.Tester.Services
{
    public class WagerServiceTests
    {
        private WagerDto CreateFakeWagerDto() =>
            new WagerDto
            {
                AccountId = Guid.NewGuid(),
                GameId = Guid.NewGuid(),
                Amount = 250,
                CreatedAt = DateTime.UtcNow
            };

        [Fact]
        public async Task CreateWager_ShouldReturnSuccess()
        {
            var dto = CreateFakeWagerDto();

            var mockMapper = new Mock<IMapper>();
            var mockPublisher = new Mock<IRabbitMqPublisher>();

            mockMapper.Setup(m => m.Map<WagerMessage>(dto)).Returns(new WagerMessage());
            mockPublisher.Setup(p => p.PublishMessage(It.IsAny<WagerMessage>())).Returns(Task.CompletedTask);

            var service = new WagerService(
                mockPublisher.Object,
                mockMapper.Object,
                Mock.Of<IUnitOfWork>(),
                Mock.Of<IWagerRepository>(),
                Mock.Of<ILogger<WagerService>>(),
                Mock.Of<ITransactionRepository>(),
                Mock.Of<IAccountRepository>());

            var result = await service.CreateWager(dto);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GetPlayerWagers_ShouldReturnPaginatedResult()
        {
            var playerId = Guid.NewGuid();
            var wagers = new List<Wager>
            {
                new Wager { Amount = 100, CreatedAt = DateTime.UtcNow },
                new Wager { Amount = 200, CreatedAt = DateTime.UtcNow }
            };

            var wagerDtos = wagers.Select(w => new WagerDto { Amount = w.Amount }).ToList();

            var mockMapper = new Mock<IMapper>();
            var mockWagerRepo = new Mock<IWagerRepository>();
            var mockUow = new Mock<IUnitOfWork>();

            mockWagerRepo.Setup(r => r.GetPlayerWagers(playerId)).ReturnsAsync(wagers);
            mockMapper.Setup(m => m.Map<IEnumerable<WagerDto>>(wagers)).Returns(wagerDtos);
            mockUow.SetupGet(u => u.Wagers).Returns(mockWagerRepo.Object);

            var service = new WagerService(
                Mock.Of<IRabbitMqPublisher>(),
                mockMapper.Object,
                mockUow.Object,
                mockWagerRepo.Object,
                Mock.Of<ILogger<WagerService>>(),
                Mock.Of<ITransactionRepository>(),
                Mock.Of<IAccountRepository>());

            var result = await service.GetPlayerWagers(playerId, 1, 10);

            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Items.Count);
        }

        [Fact]
        public async Task ProcessWager_ShouldReturnFailure_WhenAccountNotFound()
        {
            var dto = CreateFakeWagerDto();

            var mockAccountRepo = new Mock<IAccountRepository>();
            mockAccountRepo.Setup(r => r.GetAccountById(dto.AccountId)).ReturnsAsync((Account)null);

            var service = new WagerService(
                Mock.Of<IRabbitMqPublisher>(),
                Mock.Of<IMapper>(),
                Mock.Of<IUnitOfWork>(),
                Mock.Of<IWagerRepository>(),
                Mock.Of<ILogger<WagerService>>(),
                Mock.Of<ITransactionRepository>(),
                mockAccountRepo.Object);

            var result = await service.ProcessWager(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Account not found.", result.Error);
        }

        [Fact]
        public async Task ProcessWager_ShouldReturnFailure_WhenInsufficientFunds()
        {
            var dto = CreateFakeWagerDto();
            var account = new Account { Id = dto.AccountId, Balance = 50 };

            var mockAccountRepo = new Mock<IAccountRepository>();
            mockAccountRepo.Setup(r => r.GetAccountById(dto.AccountId)).ReturnsAsync(account);

            var service = new WagerService(
                Mock.Of<IRabbitMqPublisher>(),
                Mock.Of<IMapper>(),
                Mock.Of<IUnitOfWork>(),
                Mock.Of<IWagerRepository>(),
                Mock.Of<ILogger<WagerService>>(),
                Mock.Of<ITransactionRepository>(),
                mockAccountRepo.Object);

            var result = await service.ProcessWager(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Insufficient funds.", result.Error);
        }

        [Fact]
        public async Task ProcessWager_ShouldReturnSuccess_WhenValid()
        {
            var dto = CreateFakeWagerDto();
            var account = new Account { Id = dto.AccountId, Balance = 1000 };

            var mockAccountRepo = new Mock<IAccountRepository>();
            var mockUow = new Mock<IUnitOfWork>();

            mockAccountRepo.Setup(r => r.GetAccountById(dto.AccountId)).ReturnsAsync(account);
            mockUow.Setup(u => u.ExecuteInTransactionAsync(It.IsAny<Func<Task>>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync(Result.Success());
            mockUow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var service = new WagerService(
                Mock.Of<IRabbitMqPublisher>(),
                Mock.Of<IMapper>(),
                mockUow.Object,
                Mock.Of<IWagerRepository>(),
                Mock.Of<ILogger<WagerService>>(),
                Mock.Of<ITransactionRepository>(),
                mockAccountRepo.Object);

            var result = await service.ProcessWager(dto);

            Assert.True(result.IsSuccess);
        }
    }
}
