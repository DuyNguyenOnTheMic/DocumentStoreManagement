using DocumentStoreManagement.Core;
using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Queries.DocumentQueries;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.DocumentHandlers
{
    public class GetDocumentByIdHandler : IRequestHandler<GetDocumentByIdQuery, Document>
    {
        private readonly IQueryRepository<Document> _documentRepository;

        public GetDocumentByIdHandler(IQueryRepository<Document> documentRepository)
        {
            _documentRepository = documentRepository;
        }

        /// <summary>
        /// Hanlder to find document by id
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        public async Task<Document> Handle(GetDocumentByIdQuery query, CancellationToken cancellationToken)
        {
            return await _documentRepository.GetByIdAsync(CustomConstants.DocumentsTable, query.Id);
        }
    }
}
