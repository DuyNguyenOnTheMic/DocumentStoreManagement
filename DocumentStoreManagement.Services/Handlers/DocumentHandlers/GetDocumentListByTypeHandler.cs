using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models.MongoDB;
using DocumentStoreManagement.Services.Queries.DocumentQueries;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

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
            var filter = Builders<Document>.Filter.Ne(query.Type, BsonNull.Value);
            return await _documentRepository.FindAsync(filter);
        }
    }
}
