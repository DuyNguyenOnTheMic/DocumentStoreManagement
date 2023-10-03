using DocumentStoreManagement.Core.Models.MongoDB;

namespace DocumentStoreManagement.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<IEnumerable<Document>> GetAll();
        Task<Document> GetById(string id);
        Task AddNew(Document document);
        Task Delete(Document document);
    }
}
