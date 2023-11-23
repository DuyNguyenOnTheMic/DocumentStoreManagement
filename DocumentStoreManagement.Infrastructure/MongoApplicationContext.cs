using MongoDB.Driver;

namespace DocumentStoreManagement.Infrastructure
{
    /// <summary>
    /// Mongo Application Db Context interface
    /// </summary>
    public interface IMongoApplicationContext
    {
        /// <summary>
        /// Mongo database
        /// </summary>
        IMongoDatabase Database { get; }
    }

    /// <summary>
    /// Mongo Application Db Context
    /// </summary>
    public class MongoApplicationContext : IMongoApplicationContext
    {
        /// <summary>
        ///  Inject dependencies
        /// </summary>
        /// <param name="connectionSetting"></param>
        public MongoApplicationContext(IMongoDbSettings connectionSetting)
        {
            MongoClient mongoClient = new(connectionSetting.ConnectionString);
            Database = mongoClient.GetDatabase(connectionSetting.DatabaseName);
        }

        /// <inheritdoc/>
        public IMongoDatabase Database { get; }
    }
}
