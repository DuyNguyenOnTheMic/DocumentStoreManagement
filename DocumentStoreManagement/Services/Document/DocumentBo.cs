using DocumentStoreManagement.DAL;

namespace DocumentStoreManagement.Services.Document
{
    public class DocumentBo : IDocument
    {
        private readonly IGenericRepository<Models.MongoDB.Document> _documentRepository;

        public DocumentBo(IGenericRepository<Models.MongoDB.Document> documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<IEnumerable<Models.MongoDB.Document>> GetAll()
        {
            return await _documentRepository.GetAllAsync();
        }

        public async Task<Models.MongoDB.Document> GetById(string id)
        {
            return await _documentRepository.GetByIdAsync(id);
        }

        public async Task AddNew(Models.MongoDB.Document document)
        {
            await _documentRepository.AddAsync(document);
        }

        public async Task Delete(Models.MongoDB.Document document)
        {
            await _documentRepository.RemoveAsync(document);
        }
    }
}
