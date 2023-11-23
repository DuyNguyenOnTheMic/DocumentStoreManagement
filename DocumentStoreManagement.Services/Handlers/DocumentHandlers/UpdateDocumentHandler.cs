using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Commands.DocumentCommands;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.DocumentHandlers
{
    /// <inheritdoc/>
    public class UpdateDocumentHandler(IRepository<Document> documentRepository) : IRequestHandler<UpdateDocumentCommand>
    {
        private readonly IRepository<Document> _documentRepository = documentRepository;

        /// <summary>
        /// Handler to update document
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        public async Task Handle(UpdateDocumentCommand command, CancellationToken cancellationToken)
        {
            await _documentRepository.UpdateAsync(command.Document);
        }
    }
}
