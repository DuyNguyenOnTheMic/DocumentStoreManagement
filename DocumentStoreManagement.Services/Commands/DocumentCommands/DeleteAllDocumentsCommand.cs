using DocumentStoreManagement.Core.Models.MongoDB;
using MediatR;

namespace DocumentStoreManagement.Services.Commands.DocumentCommands
{
    /// <summary>
    /// Command class to delete all documents
    /// </summary>
    public record DeleteAllDocumentsCommand(IEnumerable<Document> Documents) : IRequest;
}
