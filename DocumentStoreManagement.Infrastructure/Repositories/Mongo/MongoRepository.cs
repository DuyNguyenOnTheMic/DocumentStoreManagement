using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace DocumentStoreManagement.Infrastructure.Repositories.Mongo
{
    /// <summary>
    /// MongoDB Generic Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MongoRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<T> dbSet;

        /// <summary>
        /// Inject dependencies
        /// </summary>
        /// <param name="context"></param>
        public MongoRepository(IMongoApplicationContext context)
        {
            database = context.Database;
            dbSet = database.GetCollection<T>(typeof(T).Name);
        }

        /// <inheritdoc/>
        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await dbSet.InsertOneAsync(entity, null, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await dbSet.InsertManyAsync(entities, null, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await dbSet.Find(_ => true).ToListAsync(cancellationToken);
        }

        // NOTE: Not implemented yet
        /// <inheritdoc/>
        public Task<IEnumerable<T>> GetAllWithIncludeAsync(CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<T> GetByIdAsync(object id)
        {
            return await dbSet.Find(x => x.Id == (string)id).FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> FindAsync(object expression, CancellationToken cancellationToken = default)
        {
            return await dbSet.Find((FilterDefinition<T>)expression).ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(T entity)
        {
            await dbSet.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        /// <inheritdoc/>
        public async Task RemoveAsync(T entity)
        {
            await dbSet.DeleteOneAsync(x => x.Id == entity.Id);
        }

        // NOTE: Not implemented yet
        /// <inheritdoc/>
        public Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<bool> CheckExistsAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await dbSet.Find(expression).AnyAsync(cancellationToken);
        }
    }
}
