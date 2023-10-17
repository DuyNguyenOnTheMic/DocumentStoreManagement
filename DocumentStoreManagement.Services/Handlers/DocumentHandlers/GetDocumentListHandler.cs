using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Queries.DocumentQueries;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.DocumentHandlers
{
    public class GetDocumentListHandler : IRequestHandler<GetDocumentListQuery, IEnumerable<Document>>
    {
        private readonly IRepository<Document> _documentRepository;

        public GetDocumentListHandler(IRepository<Document> documentRepository)
        {
            _documentRepository = documentRepository;
        }

        /// <summary>
        /// Handler to get all documents
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        public async Task<IEnumerable<Document>> Handle(GetDocumentListQuery query, CancellationToken cancellationToken)
        {
            return await _documentRepository.GetAllAsync();
        }
    }
}
