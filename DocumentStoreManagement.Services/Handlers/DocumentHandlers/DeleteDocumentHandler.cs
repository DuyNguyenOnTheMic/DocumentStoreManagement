using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models.MongoDB;
using DocumentStoreManagement.Services.Commands.DocumentCommands;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.DocumentHandlers
{
    public class DeleteDocumentHandler : IRequestHandler<DeleteDocumentCommand>
    {
        private readonly IGenericRepository<Document> _documentRepository;

        public DeleteDocumentHandler(IGenericRepository<Document> documentRepository)
        {
            _documentRepository = documentRepository;
        }

        /// <summary>
        /// Handler to delete document
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(DeleteDocumentCommand command, CancellationToken cancellationToken)
        {
            Document document = await _documentRepository.GetByIdAsync(command.Id);
            if (document == null)
            {
                new CancellationTokenSource().Cancel();
            }

            await _documentRepository.RemoveAsync(document);
        }
    }
}
