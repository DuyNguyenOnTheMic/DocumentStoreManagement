using DocumentStoreManagement.Core.Models;
using MediatR;

namespace DocumentStoreManagement.Services.Commands.DocumentCommands
{
    /// <summary>
    /// Command class to delete document
    /// </summary>
    /// <param name="Document"></param>
    public record DeleteDocumentCommand(Document Document) : IRequest;
}
