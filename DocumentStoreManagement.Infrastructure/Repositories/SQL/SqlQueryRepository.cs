using DocumentStoreManagement.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DocumentStoreManagement.Infrastructure.Repositories.SQL
{
    /// <summary>
    /// SQL Query Generic Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SqlQueryRepository<T> : IQueryRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public SqlQueryRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(string table)
        {
            return await _dbSet.FromSqlRaw($"SELECT * FROM {table}").ToListAsync();
        }

        public async Task<T> GetByIdAsync(string function)
        {
            return await _dbSet.FromSqlRaw($"SELECT * FROM {function}").FirstOrDefaultAsync();
        }
    }
}
