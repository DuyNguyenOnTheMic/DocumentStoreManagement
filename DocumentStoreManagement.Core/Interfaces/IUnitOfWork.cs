namespace DocumentStoreManagement.Core.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task SaveAsync();
    }
}
