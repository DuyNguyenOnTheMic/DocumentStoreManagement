using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Commands.DocumentCommands;
using DocumentStoreManagement.Services.Interfaces;
using DocumentStoreManagement.Services.Queries.DocumentQueries;
using MediatR;

namespace DocumentStoreManagement.Services
{
    /// <summary>
    /// Service class for document management
    /// </summary>
    public class DocumentService(IMediator mediator) : IDocumentService
    {
        private readonly IMediator _mediator = mediator;

        public async Task<IEnumerable<Document>> GetAll()
        {
            // Get document list
            return await _mediator.Send(new GetDocumentListQuery());
        }

        public async Task<IEnumerable<Document>> GetByType(int type)
        {
            // Get document list by type
            return await _mediator.Send(new GetDocumentListByTypeQuery(type));
        }

        public async Task<Document> GetById(string id)
        {
            // Get document by id
            return await _mediator.Send(new GetDocumentByIdQuery(id));
        }

        public async Task Update(Document document)
        {
            // Update document
            await _mediator.Send(new UpdateDocumentCommand(document));
        }

        public async Task Create<T>(T document)
        {
            // Create new document
            await _mediator.Send(new CreateDocumentCommand<T>(document));
        }

        public async Task Delete(Document document)
        {
            // Delete document
            await _mediator.Send(new DeleteDocumentCommand(document));
        }
    }
}
