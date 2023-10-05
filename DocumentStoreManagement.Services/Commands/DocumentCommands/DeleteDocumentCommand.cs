using MediatR;

namespace DocumentStoreManagement.Services.Commands.DocumentCommands
{
    /// <summary>
    /// Command class to delete document
    /// </summary>
    public record DeleteDocumentCommand(string Id) : IRequest;
}
