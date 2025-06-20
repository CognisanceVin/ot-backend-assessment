using Microsoft.Extensions.Logging;
using OT.Assessment.Application;
using OT.Assessment.Application.Common;

namespace OT.Assessment.Infrastructure.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OTAssessmentDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(OTAssessmentDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<Result> ExecuteInTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await action(); // Perform repo actions
                var affected = await _context.SaveChangesAsync(cancellationToken);

                if (affected == 0)
                    return Result.Failure("No changes were committed to the database.");

                await transaction.CommitAsync(cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Transaction failed");
                await transaction.RollbackAsync(cancellationToken);
                return Result.Failure("Transaction failed: " + ex.Message);
            }
        }
    }
}
