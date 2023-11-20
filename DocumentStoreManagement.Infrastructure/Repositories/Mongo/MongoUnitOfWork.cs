using DocumentStoreManagement.Core.Interfaces;

namespace DocumentStoreManagement.Infrastructure.Repositories.Mongo
{
    /// <summary>
    /// Encapsulates all repository transactions
    /// </summary>
    public class MongoUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Add an empty method as MongoDb doesn't need saving
        /// </summary>
        public async Task SaveAsync() => await Task.CompletedTask;

        public async Task RefreshMaterializedViewAsync(string viewName) => await Task.CompletedTask;

        /// <summary>
        /// Cleans up any resources being used.
        /// </summary>
        public async ValueTask DisposeAsync()
        {
            // Take this object off the finalization queue to prevent 
            // finalization code for this object from executing a second time.
            GC.SuppressFinalize(this);
            await Task.CompletedTask;
        }
    }
}
