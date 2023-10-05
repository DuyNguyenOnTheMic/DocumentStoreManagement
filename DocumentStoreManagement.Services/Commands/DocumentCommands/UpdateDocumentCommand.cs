using DocumentStoreManagement.Core.Models.MongoDB;
using MediatR;

namespace DocumentStoreManagement.Services.Commands.DocumentCommands
{
    /// <summary>
    /// Command class to update document
    /// </summary>
    public record UpdateDocumentCommand(Document Document) : IRequest;
}
