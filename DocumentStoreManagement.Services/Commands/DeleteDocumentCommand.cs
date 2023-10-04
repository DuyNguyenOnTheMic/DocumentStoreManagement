using MediatR;

namespace DocumentStoreManagement.Services.Commands
{
    public class DeleteDocumentCommand : IRequest<int>
    {
        public string Id { get; set; }
    }
}
