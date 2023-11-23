using DocumentStoreManagement.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DocumentStoreManagement.Infrastructure.Repositories.SQL
{
    /// <summary>
    /// SQL Generic Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SqlRepository<T>(DbContext dbContext) : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext = dbContext;
        private readonly DbSet<T> _dbSet = dbContext.Set<T>();
        private static readonly Func<DbContext, IAsyncEnumerable<T>> testQuery = EF.CompileAsyncQuery((DbContext db) => db.Set<T>());

        /// <summary>
        /// Test query to select all data from database
        /// </summary>
        public async Task<IEnumerable<T>> GetAllTestAsync()
        {
            return await Task.FromResult(testQuery(_dbContext).ToBlockingEnumerable().ToList());
        }

        /// <inheritdoc/>
        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetAllWithIncludeAsync(CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return await query.ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> FindAsync(object expression, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where((Expression<Func<T, bool>>)expression).ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(T entityToUpdate)
        {
            _dbContext.Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public async Task<bool> CheckExistsAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(expression, cancellationToken);
        }
    }
}
