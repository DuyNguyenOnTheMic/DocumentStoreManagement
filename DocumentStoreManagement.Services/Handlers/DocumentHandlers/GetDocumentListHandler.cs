using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models.MongoDB;
using DocumentStoreManagement.Services.Queries.DocumentQueries;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.DocumentHandlers
{
    public class GetDocumentListHandler : IRequestHandler<GetDocumentListQuery, IEnumerable<Document>>
    {
        private readonly IGenericRepository<Document> _documentRepository;

        public GetDocumentListHandler(IGenericRepository<Document> documentRepository)
        {
            _documentRepository = documentRepository;
        }

        /// <summary>
        /// Handler to get all documents
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Document>> Handle(GetDocumentListQuery query, CancellationToken cancellationToken)
        {
            return await _documentRepository.GetAllAsync();
        }
    }
}
