using OT.Assessment.Application.Common;

namespace OT.Assessment.Application
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<Result> ExecuteInTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default);
    }
}
