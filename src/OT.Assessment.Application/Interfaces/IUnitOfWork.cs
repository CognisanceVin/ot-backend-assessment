using OT.Assessment.Application.Common;
using OT.Assessment.Domain.Interfaces.Repositories;

namespace OT.Assessment.Application
{
    public interface IUnitOfWork
    {
        IPlayerRepository Players { get; }
        IAccountRepository Accounts { get; }
        IWagerRepository Wagers { get; }
        IGameRepository Games { get; }
        ITransactionRepository Transactions { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<Result> ExecuteInTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default);

    }
}
