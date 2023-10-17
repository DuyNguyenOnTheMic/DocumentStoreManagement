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

        public async Task<IEnumerable<T>> GetAllAsync(string function)
        {
            return await _dbSet.FromSqlRaw($"SELECT * FROM {function}").ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbSet.FromSqlRaw($"SELECT * FROM get_document_by_id('{id}')").FirstOrDefaultAsync();
        }

    }
}
