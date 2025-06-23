using OT.Assessment.Domain.Entities.AuditTrail;

namespace OT.Assessment.Domain.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task AddRecord(Transaction record);
        Task<IEnumerable<Transaction>> GetAllTransactionRecords();
        Task<IEnumerable<Transaction>> GetTransactionRecordsForEntity(string entity);
        Task<IEnumerable<Transaction>> GetTransactionRecordsForEntityRecord(string entity, Guid entityId);
    }
}
