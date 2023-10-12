using DocumentStoreManagement.Core.Models;

namespace DocumentStoreManagement.Services.Interfaces
{
    /// <summary>
    /// Interface for document service
    /// </summary>
    public interface IDocumentService
    {
        Task<IEnumerable<Document>> GetAll();
        Task<IEnumerable<Document>> GetByType(int type);
        Task<Document> GetById(string id);
        Task Create<T>(T document);
        Task Update(Document document);
        Task Delete(Document document);
    }
}
