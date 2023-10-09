using DocumentStoreManagement.Core;
using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Queries.DocumentQueries;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.DocumentHandlers
{
    public class GetDocumentListByTypeHandler : IRequestHandler<GetDocumentListByTypeQuery, IEnumerable<Document>>
    {
        private readonly IGenericRepository<Document> _documentRepository;

        public GetDocumentListByTypeHandler(IGenericRepository<Document> documentRepository)
        {
            _documentRepository = documentRepository;
        }

        /// <summary>
        /// Handler to get documents by type
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        public async Task<IEnumerable<Document>> Handle(GetDocumentListByTypeQuery query, CancellationToken cancellationToken)
        {
            string type = CustomConstants.DocumentTypes[query.Type];
            return await _documentRepository.FindByTypeAsync(type);
        }
    }
}
