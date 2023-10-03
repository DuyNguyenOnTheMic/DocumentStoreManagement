namespace DocumentStoreManagement.Services.Document
{
    public interface IDocument
    {
        Task<IEnumerable<Models.MongoDB.Document>> GetAll();
        Task<Models.MongoDB.Document> GetById(string id);
        Task AddNew(Models.MongoDB.Document document);
        Task Delete(Models.MongoDB.Document document);
    }
}
