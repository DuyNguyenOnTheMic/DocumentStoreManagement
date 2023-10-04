using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models.MongoDB;
using DocumentStoreManagement.Services.Interfaces;

namespace DocumentStoreManagement.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IGenericRepository<Document> _documentRepository;

        public DocumentService(IGenericRepository<Document> documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<IEnumerable<Document>> GetAll()
        {
            return await _documentRepository.GetAllAsync();
        }

        public async Task<Document> GetById(string id)
        {
            return await _documentRepository.GetByIdAsync(id);
        }

        public async Task Update(Document document)
        {
            await _documentRepository.UpdateAsync(document);
        }

        public async Task Create(Document document)
        {
            document.Book = new Book
            {
                AuthorName = "bà ơi bà ơi",
                PageNumber = 1
            };
            await _documentRepository.AddAsync(document);
        }

        public async Task Delete(Document document)
        {
            await _documentRepository.RemoveAsync(document);
        }
    }
}
