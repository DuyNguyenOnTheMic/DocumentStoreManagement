namespace DocumentStoreManagement.Core.Interfaces
{
    /// <summary>
    /// Generic Unit Of Work interface
    /// </summary>
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task SaveAsync();
    }
}
