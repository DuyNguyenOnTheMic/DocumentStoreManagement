using MediatR;

namespace DocumentStoreManagement.Services.Commands.DocumentCommands
{
    /// <summary>
    /// Command class to delete document
    /// </summary>
    public class DeleteDocumentCommand : IRequest
    {
        public string Id { get; set; }
    }
}
