using DocumentStoreManagement.Core;
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
            await _mediator.Send(new UpdateDocumentCommand(document));
        }

        public async Task Create(Document document)
        {
            await _mediator.Send(new CreateDocumentCommand(document));
        }

        public async Task Delete(string id)
        {
            await _mediator.Send(new DeleteDocumentCommand(id));
        }
    }
}
