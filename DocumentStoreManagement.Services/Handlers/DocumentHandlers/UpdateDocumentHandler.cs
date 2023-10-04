using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models.MongoDB;
using DocumentStoreManagement.Services.Commands.DocumentCommands;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.DocumentHandlers
{
    public class UpdateDocumentHandler : IRequestHandler<UpdateDocumentCommand>
    {
        private readonly IGenericRepository<Document> _documentRepository;

        public UpdateDocumentHandler(IGenericRepository<Document> documentRepository)
        {
            _documentRepository = documentRepository;
        }

        /// <summary>
        /// Handler to update document
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(UpdateDocumentCommand command, CancellationToken cancellationToken)
        {
            var document = await _documentRepository.GetByIdAsync(command.Id);
            if (document == null)
            {
                // Cancel request if document not found
                var cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.Cancel();
            }

            // Set properties to object for updating request
            document.PublisherName = command.PublisherName;
            document.ReleaseQuantity = command.ReleaseQuantity;
            document.Book = command.Book;
            document.Magazine = command.Magazine;
            document.Newspaper = command.Newspaper;

            await _documentRepository.UpdateAsync(document);
        }
    }
}
