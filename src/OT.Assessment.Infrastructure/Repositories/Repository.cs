using Microsoft.EntityFrameworkCore;
using OT.Assessment.Domain.Entities.Common;
using OT.Assessment.Infrastructure.Persistance;

namespace OT.Assessment.Infrastructure.Repositories
{
    public class Repository<TEntity> where TEntity : EntityBase
    {
        protected readonly OTAssessmentDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(OTAssessmentDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public virtual async Task<TEntity?> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        public virtual async Task Add(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public virtual async Task Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetById(id, cancellationToken);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

    }
}
