using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Commands.DocumentCommands;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.DocumentHandlers
{
    /// <inheritdoc/>
    public class DeleteDocumentHandler(IRepository<Document> documentRepository) : IRequestHandler<DeleteDocumentCommand>
    {
        private readonly IRepository<Document> _documentRepository = documentRepository;

        /// <summary>
        /// Handler to delete document
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        public async Task Handle(DeleteDocumentCommand command, CancellationToken cancellationToken)
        {
            await _documentRepository.RemoveAsync(command.Document);
        }
    }
}
