using DocumentStoreManagement.Models.MongoDB;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace DocumentStoreManagement.DAL.Mongo
{
    /// <summary>
    /// A non-instantiable base entity which defines members available across all entities
    /// </summary>
    public abstract class EntityBase
    {
        public string Id { get; set; }
    }

    public class MongoGenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly IMongoDatabase database;
        protected readonly IMongoCollection<T> dbSet;

        public MongoGenericRepository(IMongoApplicationContext context)
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

        public async Task<T> GetByIdAsync(object id)
        {
            return await dbSet.Find(FilterId(id)).FirstOrDefaultAsync();
        }

        public IEnumerable<T> FindAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(T entity)
        {
            await dbSet.DeleteOneAsync(FilterId(entity));
        }

        public Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(T entity)
        {
            await dbSet.ReplaceOneAsync(FilterId(entity), entity);
        }

        public async Task<bool> CheckExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await dbSet.Find(expression).AnyAsync();
        }

        private static FilterDefinition<T> FilterId(object key)
        {
            return Builders<T>.Filter.Eq("Id", key);
        }
    }
}
