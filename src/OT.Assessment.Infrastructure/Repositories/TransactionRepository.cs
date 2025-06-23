using Microsoft.EntityFrameworkCore;
using OT.Assessment.Domain.Entities.AuditTrail;
using OT.Assessment.Domain.Interfaces.Repositories;
using OT.Assessment.Infrastructure.Persistance;

namespace OT.Assessment.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly OTAssessmentDbContext _context;

        public TransactionRepository(OTAssessmentDbContext context)
        {
            _context = context;
        }

        public async Task AddRecord(Transaction record)
        {
            await _context.TransactionRecords.AddAsync(record);
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionRecords()
        {
            return await _context.TransactionRecords.ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionRecordsForEntity(string entity)
        {
            return await _context.TransactionRecords.Where(i => i.EntityType == entity).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionRecordsForEntityRecord(string entity, Guid entityId)
        {
            return await _context.TransactionRecords.Where(i => i.EntityType == entity && i.EntityId == entityId).ToListAsync();
        }
    }
}
