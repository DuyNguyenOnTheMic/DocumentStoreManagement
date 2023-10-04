using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models.MongoDB;
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
        /// <returns></returns>
        public async Task<Document> Handle(CreateDocumentCommand command, CancellationToken cancellationToken)
        {
            var document = new Document()
            {
                PublisherName = command.PublisherName,
                ReleaseQuantity = command.ReleaseQuantity,
                Book = command.Book,
                Magazine = command.Magazine,
                Newspaper = command.Newspaper
            };

            await _documentRepository.AddAsync(document);
            return document;
        }
    }
}
