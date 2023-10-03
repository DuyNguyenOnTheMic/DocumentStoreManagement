using DocumentStoreManagement.Core.Models.MongoDB;

namespace DocumentStoreManagement.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<IEnumerable<Document>> GetAll();
        Task<Document> GetById(string id);
        Task Create(Document document);
        Task Update(Document document);
        Task Delete(Document document);
    }
}
