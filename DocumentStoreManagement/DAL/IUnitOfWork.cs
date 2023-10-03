namespace DocumentStoreManagement.DAL
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task SaveAsync();
    }
}
