using OT.Assessment.Domain.Entities.AuditTrail;

namespace OT.Assessment.Domain.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task AddRecord(TransactionRecord record);
        Task<IEnumerable<TransactionRecord>> GetAllTransactionRecords();
        Task<IEnumerable<TransactionRecord>> GetTransactionRecordsForEntity(string entity);
        Task<IEnumerable<TransactionRecord>> GetTransactionRecordsForEntityRecord(string entity, Guid entityId);
    }
}
