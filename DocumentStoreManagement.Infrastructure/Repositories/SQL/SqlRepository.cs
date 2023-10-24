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
        protected readonly DbContext _dbContext = dbContext;
        protected readonly DbSet<T> _dbSet = dbContext.Set<T>();

        /// <summary>
        /// Test query to select all data from database
        /// </summary>
        private static readonly Func<DbContext, IAsyncEnumerable<T>> testQuery = EF.CompileAsyncQuery((DbContext db) => db.Set<T>());
        public async Task<IEnumerable<T>> GetAllTestAsync()
        {
            return await Task.FromResult(testQuery(_dbContext).ToBlockingEnumerable().ToList());
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindAsync(object expression)
        {
            return await _dbSet.Where((Expression<Func<T, bool>>)expression).ToListAsync();
        }

        public Task UpdateAsync(T entityToUpdate)
        {
            _dbContext.Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public async Task<bool> CheckExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }
    }
}
