namespace DocumentStoreManagement.Infrastructure
{
    /// <summary>
    /// MongoDb Settings
    /// </summary>
    public class MongoDbSettings : IMongoDbSettings
    {
        /// <inheritdoc/>
        public string ConnectionString { get; set; }

        /// <inheritdoc/>
        public string DatabaseName { get; set; }
    }

    /// <summary>
    /// MongoDb Settings interface
    /// </summary>
    public interface IMongoDbSettings
    {
        /// <summary>
        /// Database connections string
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Database name
        /// </summary>
        string DatabaseName { get; set; }
    }
}
