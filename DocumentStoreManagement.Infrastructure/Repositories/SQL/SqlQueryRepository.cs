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

        public async Task<IEnumerable<T>> Get(string query)
        {
            return await _dbContext.Database.SqlQueryRaw<T>(query).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(string table)
        {
            FormattableString query = FormattableStringFactory.Create(
                $"SELECT * FROM {table}"
            );
            return await _dbSet.FromSql(query).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithIncludeAsync(string table, string includeTable)
        {
            FormattableString query = FormattableStringFactory.Create(
                $"SELECT * FROM {table}"
            );
            return await _dbSet.FromSql(query).Include(includeTable).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetBetweenDatesAsync(string table, string column, string from, string to)
        {
            FormattableString query = FormattableStringFactory.Create(
                $"SELECT * FROM {table} "
                + $@"WHERE ""{column}"" "
                + $"BETWEEN '{from}' AND '{to}'"
            );
            return await _dbSet.FromSql(query).ToListAsync();
        }

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
