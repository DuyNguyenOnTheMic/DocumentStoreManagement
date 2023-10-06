using DocumentStoreManagement.Core.Models.MongoDB;
using DocumentStoreManagement.Helpers;
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

        public async Task<IEnumerable<Document>> GetByType(int type)
        {
            bool hasKeyValue = CustomConstants.DocumentTypes.ContainsKey(type);
            if (hasKeyValue)
            {
                // Get document list by type
                return await _mediator.Send(new GetDocumentListByTypeQuery(type));
            }
            else
            {
                // Throw error
                throw new Exception("The input type is not valid, please try again!");
            }
        }

        public async Task<Document> GetById(string id)
        {
            return await _mediator.Send(new GetDocumentByIdQuery(id));
        }

        public async Task Update(Document document)
        {
            // Check if document only have one document type
            if (HasOnlyOneDocument(document))
            {
                // Update document
                await _mediator.Send(new UpdateDocumentCommand(document));
            }
            else
            {
                // Throw error
                throw new MongoException("Only one document type is allowed!");
            }
        }

        public async Task Create(Document document)
        {
            // Check if document only have one document type
            if (HasOnlyOneDocument(document))
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

        /// <summary>
        /// Only one document is allowed in request
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static bool HasOnlyOneDocument(Document document)
        {
            // Check that only one document type is passed in
            object[] objects = { document.Book, document.Magazine, document.Newspaper };
            int trueCount = objects.Count(x => x != null);

            return trueCount == 1;
        }
    }
}
