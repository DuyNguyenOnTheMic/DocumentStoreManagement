using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models.MongoDB;
using DocumentStoreManagement.Services.Queries;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers
{
    public class GetDocumentByIdHandler : IRequestHandler<GetDocumentByIdQuery, Document>
    {
        private readonly IGenericRepository<Document> _documentRepository;

        public GetDocumentByIdHandler(IGenericRepository<Document> documentRepository)
        {
            _documentRepository = documentRepository;
        }

        /// <summary>
        /// Hanlder to find document by id
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Document> Handle(GetDocumentByIdQuery query, CancellationToken cancellationToken)
        {
            return await _documentRepository.GetByIdAsync(query.Id);
        }
    }
}
