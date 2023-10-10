using MediatR;

namespace DocumentStoreManagement.Services.Commands.DocumentCommands
{
    /// <summary>
    /// Command class to create document
    /// </summary>
    public record CreateDocumentCommand<T>(T Document) : IRequest<T>;
}
