using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace DocumentStoreManagement.Infrastructure.Repositories.Mongo
{
    /// <summary>
    /// Mongodb Generic Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MongoRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly IMongoDatabase database;
        protected readonly IMongoCollection<T> dbSet;

        public MongoRepository(IMongoApplicationContext context)
        {
            database = context.Database;
            dbSet = database.GetCollection<T>(typeof(T).Name);
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.InsertOneAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await dbSet.InsertManyAsync(entities);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.Find(_ => true).ToListAsync();
        }

        // NOTE: Not implemented yet
        public Task<IEnumerable<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await dbSet.Find(x => x.Id == (string)id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(object expression)
        {
            return await dbSet.Find((FilterDefinition<T>)expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByTypeAsync(string type)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_t", type);
            return await dbSet.Find(filter).ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await dbSet.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        public async Task RemoveAsync(T entity)
        {
            await dbSet.DeleteOneAsync(x => x.Id == entity.Id);
        }

        // NOTE: Not implemented yet
        public Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await dbSet.Find(expression).AnyAsync();
        }
    }
}
