using DocumentStoreManagement.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DocumentStoreManagement.Infrastructure.Repositories.SQL
{
    /// <summary>
    /// SQL Generic Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SqlGenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public SqlGenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<IEnumerable<T>> FindAsync(object expression)
        {
            return await _dbSet.Where((Expression<Func<T, bool>>)expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // NOTE: Not implemented yet
        public Task<IEnumerable<T>> GetByTypeAsync(object filter)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
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
