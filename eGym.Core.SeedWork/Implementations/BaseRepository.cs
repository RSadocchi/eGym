using eGym.Core.SeedWork.NSpecifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGym.Core.SeedWork
{
    public abstract class BaseRepository<TDbContext, TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : Entity, IAggregateRoot
        where TDbContext : DbContext, IUnitOfWork
    {
        protected readonly TDbContext _context;
        public IUnitOfWork UnitOfWork { get => _context; }

        public BaseRepository(TDbContext context) { _context = context ?? throw new ArgumentNullException(nameof(context)); }

        public TEntity Find(TPrimaryKey id) => _context.Set<TEntity>().Find(id);
        public async Task<TEntity> FindAsync(TPrimaryKey id) => await _context.Set<TEntity>().FindAsync(id);

        public IQueryable<TEntity> GetAll() => _context.Set<TEntity>().AsQueryable();
        public async Task<IQueryable<TEntity>> GetAllAsync() => await Task.FromResult(_context.Set<TEntity>().AsQueryable());

        public IQueryable<TEntity> GetBySpecification(ISpecification<TEntity> spec = null, bool tracking = true)
        {
            var query = _context.Set<TEntity>().Select(r => r);
            if (!tracking)
                query = query.AsNoTracking();
            if (spec != null)
                query = query.Where(spec.Expression);
            return query;
        }

        public TEntity Add(TEntity entity) { return _context.Set<TEntity>().Add(entity).Entity; }
        public Task<TEntity> AddAsync(TEntity entity) { return Task.FromResult(Add(entity)); }

        public void AddRange(IEnumerable<TEntity> entities) { _context.Set<TEntity>().AddRange(entities); }
        public Task AddRangeAsync(IEnumerable<TEntity> entities) { AddRange(entities); return Task.CompletedTask; }

        public TEntity Update(TEntity entity) { _context.Entry(entity).State = EntityState.Modified; return entity; }
        public Task<TEntity> UpdateAsync(TEntity entity) { return Task.FromResult(Update(entity)); }

        public void Remove(TEntity entity) { _context.Set<TEntity>().Remove(entity); }
        public Task RemoveAsync(TEntity entity) { Remove(entity); return Task.CompletedTask; }

        public void RemoveRange(IEnumerable<TEntity> entities) { _context.Set<TEntity>().RemoveRange(entities); }
        public Task RemoveRangeAsync(IEnumerable<TEntity> entities) { RemoveRange(entities); return Task.CompletedTask; }

        public TEntity Attach(TEntity entity) { return _context.Set<TEntity>().Attach(entity).Entity; }
        public Task<TEntity> AttachAsync(TEntity entity) { return Task.FromResult(Attach(entity)); }

        public void Detach(TEntity entity) { _context.Entry(entity).State = EntityState.Detached; }
        public Task DetachAsync(TEntity entity) { Detach(entity); return Task.CompletedTask; }
    }
}
