using eGym.Core.SeedWork.NSpecifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGym.Core.SeedWork
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : class, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        TEntity Find(TPrimaryKey id);
        Task<TEntity> FindAsync(TPrimaryKey id);

        IQueryable<TEntity> GetAll();
        Task<IQueryable<TEntity>> GetAllAsync();

        IQueryable<TEntity> GetBySpecification(ISpecification<TEntity> spec = null, bool tracking = true);

        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);

        void Remove(TEntity entity);
        Task RemoveAsync(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);
        Task RemoveRangeAsync(IEnumerable<TEntity> entities);

        TEntity Attach(TEntity entity);
        Task<TEntity> AttachAsync(TEntity entity);

        void Detach(TEntity entity);
        Task DetachAsync(TEntity entity);
    }
}
