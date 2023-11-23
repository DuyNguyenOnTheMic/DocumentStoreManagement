﻿using Dapper;
using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using System.Data;

namespace DocumentStoreManagement.Infrastructure.Repositories.SQL
{
    /// <summary>
    /// SQL Query Generic Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SqlQueryRepository<T>(IDbConnection db) : IQueryRepository<T> where T : class
    {
        private readonly IDbConnection _db = db;

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetAsync(string query)
        {
            return await _db.QueryAsync<T>(query);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetAllAsync(string table)
        {
            string query = $"SELECT * FROM {table}";
            return await _db.QueryAsync<T>(query);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetByDiscriminator(string table)
        {
            string query = $@"SELECT * FROM {table} 
                            WHERE ""Discriminator"" = '{typeof(T).Name}'";
            return await _db.QueryAsync<T>(query);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetBetweenDatesAsync(string table, string column, string from, string to)
        {
            string query = $@"SELECT * FROM {table}
                            WHERE ""{column}""
                            BETWEEN '{from}' AND '{to}'";
            return await _db.QueryAsync<T>(query);
        }

        /// <inheritdoc/>
        public async Task<T> GetByIdAsync(string table, object id)
        {
            string query = $@"SELECT * FROM {table}
                            WHERE ""{nameof(BaseEntity.Id)}"" = '{id}'";
            return await _db.QueryFirstOrDefaultAsync<T>(query);
        }
    }
}
