using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Commands.DocumentCommands;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.DocumentHandlers
{
    public class CreateDocumentHandler : IRequestHandler<CreateDocumentCommand, Document>
    {
        private readonly IGenericRepository<Document> _documentRepository;

        public CreateDocumentHandler(IGenericRepository<Document> documentRepository)
        {
            _documentRepository = documentRepository;
        }

        /// <summary>
        /// Handler to create new document
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        public async Task<Document> Handle(CreateDocumentCommand command, CancellationToken cancellationToken)
        {
            await _documentRepository.AddAsync(command.Document);
            return command.Document;
        }
    }
}
