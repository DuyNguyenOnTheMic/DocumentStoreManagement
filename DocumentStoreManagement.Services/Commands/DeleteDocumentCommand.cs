using MediatR;

namespace DocumentStoreManagement.Services.Commands
{
    /// <summary>
    /// Command class to delete document
    /// </summary>
    public class DeleteDocumentCommand : IRequest
    {
        public string Id { get; set; }
    }
}
