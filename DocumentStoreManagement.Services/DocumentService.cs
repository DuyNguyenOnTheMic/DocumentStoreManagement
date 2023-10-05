using DocumentStoreManagement.Core.Models.MongoDB;
using DocumentStoreManagement.Services.Commands.DocumentCommands;
using DocumentStoreManagement.Services.Interfaces;
using DocumentStoreManagement.Services.Queries.DocumentQueries;
using MediatR;

namespace DocumentStoreManagement.Services
{
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
            return await _mediator.Send(new GetDocumentByIdQuery() { Id = id });
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
            await _mediator.Send(new DeleteDocumentCommand() { Id = id });
        }
    }
}
