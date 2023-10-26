namespace DocumentStoreManagement.Core.Interfaces
{
    /// <summary>
    /// Generic Unit Of Work interface
    /// </summary>
    public interface IUnitOfWork : IAsyncDisposable
    {
        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns>Nothing</returns>
        Task SaveAsync();

        /// <summary>
        /// Refresh Materialized View
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns>Nothing</returns>
        Task RefreshMaterializedViewAsync(string viewName);
    }
}
