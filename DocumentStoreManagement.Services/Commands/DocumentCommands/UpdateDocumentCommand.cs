using DocumentStoreManagement.Core.Models;
using MediatR;

namespace DocumentStoreManagement.Services.Commands.DocumentCommands
{
    /// <summary>
    /// Command class to update document
    /// </summary>
    /// <param name="Document"></param>
    public record UpdateDocumentCommand(Document Document) : IRequest;
}
