using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Commands.DocumentCommands;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.DocumentHandlers
{
    public class UpdateDocumentHandler : IRequestHandler<UpdateDocumentCommand>
    {
        private readonly IRepository<Document> _documentRepository;

        public UpdateDocumentHandler(IRepository<Document> documentRepository)
        {
            _documentRepository = documentRepository;
        }

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
