using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models.MongoDB;
using DocumentStoreManagement.Services.Commands.DocumentCommands;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.DocumentHandlers
{
    public class DeleteAllDocumentsHandler : IRequestHandler<DeleteAllDocumentsCommand>
    {
        private readonly IGenericRepository<Document> _documentRepository;

        public DeleteAllDocumentsHandler(IGenericRepository<Document> documentRepository)
        {
            _documentRepository = documentRepository;
        }

        /// <summary>
        /// Handle to delete all documents
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(DeleteAllDocumentsCommand request, CancellationToken cancellationToken)
        {
            await _documentRepository.RemoveRangeAsync(request.Documents);
        }
    }
}
