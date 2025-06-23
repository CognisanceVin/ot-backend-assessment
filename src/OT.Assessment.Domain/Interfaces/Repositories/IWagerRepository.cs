using OT.Assessment.Domain.Entities;

namespace OT.Assessment.Domain.Interfaces.Repositories
{
    public interface IWagerRepository
    {
        Task<bool> AddWager(Wager wager);
        //IUnitOfWork UnitOfWork { get; }
    }
}
