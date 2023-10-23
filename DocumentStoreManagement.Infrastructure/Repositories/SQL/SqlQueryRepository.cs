using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace DocumentStoreManagement.Infrastructure.Repositories.SQL
{
    /// <summary>
    /// SQL Query Generic Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SqlQueryRepository<T>(DbContext dbContext) : IQueryRepository<T> where T : class
    {
        protected readonly DbContext _dbContext = dbContext;
        protected readonly DbSet<T> _dbSet = dbContext.Set<T>();

        /// <summary>
        /// Gets by query and returns as DTO
        /// </summary>
        /// <param name="query"></param>
        /// <returns>List of data</returns>
        public async Task<IEnumerable<T>> Get(string query)
        {
            return await _dbContext.Database.SqlQueryRaw<T>(query).ToListAsync();
        }

        /// <summary>
        /// Gets all the data from a table
        /// </summary>
        /// <param name="table"></param>
        /// <returns>List of data</returns>
        public async Task<IEnumerable<T>> GetAllAsync(string table)
        {
            FormattableString query = FormattableStringFactory.Create(
                $"SELECT * FROM {table}"
            );
            return await _dbSet.FromSql(query).ToListAsync();
        }

        /// <summary>
        /// Gets all the data from a table include with a child table
        /// </summary>
        /// <param name="table"></param>
        /// <param name="includeTable"></param>
        /// <returns>List of data</returns>
        public async Task<IEnumerable<T>> GetAllWithIncludeAsync(string table, string includeTable)
        {
            FormattableString query = FormattableStringFactory.Create(
                $"SELECT * FROM {table}"
            );
            return await _dbSet.FromSql(query).Include(includeTable).ToListAsync();
        }

        /// <summary>
        /// Gets all the data from a table which between 2 dates
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>A list of data</returns>
        public async Task<IEnumerable<T>> GetBetweenDatesAsync(string table, string column, string from, string to)
        {
            FormattableString query = FormattableStringFactory.Create(
                $"SELECT * FROM {table} "
                + $@"WHERE ""{column}"" "
                + $"BETWEEN '{from}' AND '{to}'"
            );
            return await _dbSet.FromSql(query).ToListAsync();
        }

        /// <summary>
        /// Gets an item by its Id
        /// </summary>
        /// <param name="table"></param>
        /// <param name="id"></param>
        /// <returns>An item which matches the Id</returns>
        public async Task<T> GetByIdAsync(string table, object id)
        {
            FormattableString query = FormattableStringFactory.Create(
                $"SELECT * FROM {table} "
                + $@"WHERE ""{nameof(BaseEntity.Id)}"" = '{id}'"
            );
            return await _dbSet.FromSql(query).FirstOrDefaultAsync();
        }
    }
}
