using DocumentStoreManagement.Core.Models;

namespace DocumentStoreManagement.Services.Interfaces
{
    /// <summary>
    /// Interface for document service
    /// </summary>
    public interface IDocumentService
    {
        /// <summary>
        /// Get all documents
        /// </summary>
        /// <returns>A list of documents</returns>
        Task<IEnumerable<Document>> GetAll();

        /// <summary>
        /// Get documents by type
        /// </summary>
        /// <param name="type"></param>
        /// <returns>A list of documents which matches the type</returns>
        Task<IEnumerable<Document>> GetByType(int type);

        /// <summary>
        /// Find document by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An document</returns>
        Task<Document> GetById(string id);

        /// <summary>
        /// Create new document
        /// </summary>
        /// <param name="document"></param>
        /// <returns>A newly created document</returns>
        Task Create<T>(T document);

        /// <summary>
        /// Update an document
        /// </summary>
        /// <param name="document"></param>
        /// <returns>Nothing</returns>
        Task Update(Document document);

        /// <summary>
        /// Delete an document
        /// </summary>
        /// <param name="document"></param>
        /// <returns>Nothing</returns>
        Task Delete(Document document);
    }
}
