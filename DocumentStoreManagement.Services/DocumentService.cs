using DocumentStoreManagement.Core.Models.MongoDB;
using DocumentStoreManagement.Services.Commands.DocumentCommands;
using DocumentStoreManagement.Services.Interfaces;
using DocumentStoreManagement.Services.Queries.DocumentQueries;
using MediatR;
using MongoDB.Driver;

namespace DocumentStoreManagement.Services
{
    /// <summary>
    /// Service class for document management
    /// </summary>
    public class DocumentService : IDocumentService
    {
        private readonly IMediator _mediator;

        public DocumentService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<Document>> GetAll()
        {
            return await _mediator.Send(new GetDocumentListQuery());
        }

        public async Task<Document> GetById(string id)
        {
            return await _mediator.Send(new GetDocumentByIdQuery(id));
        }

        public async Task Update(Document document)
        {
            await _mediator.Send(new UpdateDocumentCommand(document));
        }

        public async Task Create(Document document)
        {
            // Check that only one document type is passed in
            bool hasOneDocument = OnlyOneObjectNotNull(document.Book, document.Magazine, document.Newspaper);
            if (hasOneDocument)
            {
                // Create document
                await _mediator.Send(new CreateDocumentCommand(document));
            }
            else
            {
                // Throw error
                throw new MongoException("Only one document type is allowed!");
            }
        }

        public async Task Delete(string id)
        {
            await _mediator.Send(new DeleteDocumentCommand(id));
        }

        public async Task DeleteAll(IEnumerable<Document> documents)
        {
            await _mediator.Send(new DeleteAllDocumentsCommand(documents));
        }

        /// <summary>
        /// Only one object is not null
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        public static bool OnlyOneObjectNotNull(params object[] objects)
        {
            int trueCount = objects.Count(x => x != null);

            return trueCount == 1;
        }
    }
}
