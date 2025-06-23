// Infrastructure Layer
using OT.Assessment.Application;
using OT.Assessment.Application.Common;
using OT.Assessment.Domain.Interfaces.Repositories;
using OT.Assessment.Infrastructure.Persistance;

namespace OT.Assessment.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OTAssessmentDbContext _context;

        public IPlayerRepository Players { get; }
        public IAccountRepository Accounts { get; }
        public IWagerRepository Wagers { get; }
        public ITransactionRepository Transactions { get; }
        public IGameRepository Games { get; }

        public UnitOfWork(OTAssessmentDbContext context,
                          IPlayerRepository playerRepository,
                          IAccountRepository accountRepository,
                          IWagerRepository wagerRepository,
                          ITransactionRepository transactionRepository,
                          IGameRepository gameRepository)
        {
            _context = context;
            Players = playerRepository;
            Accounts = accountRepository;
            Wagers = wagerRepository;
            Transactions = transactionRepository;
            Games = gameRepository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Result> ExecuteInTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await action();
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result.Failure(ex.Message);
            }
        }
    }
}
