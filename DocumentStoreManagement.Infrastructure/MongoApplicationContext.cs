using MongoDB.Driver;

namespace DocumentStoreManagement.Infrastructure
{
    public interface IMongoApplicationContext
    {
        IMongoDatabase Database { get; }
    }

    public class MongoApplicationContext : IMongoApplicationContext
    {
        public MongoApplicationContext(IMongoDbSettings connectionSetting)
        {
            var mongoClient = new MongoClient(connectionSetting.ConnectionString);
            Database = mongoClient.GetDatabase(connectionSetting.DatabaseName);
        }

        public IMongoDatabase Database { get; }
    }
}
