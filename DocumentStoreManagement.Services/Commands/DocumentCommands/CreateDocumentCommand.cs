using MediatR;

namespace DocumentStoreManagement.Services.Commands.DocumentCommands
{
    /// <summary>
    /// Command class to create document
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Document"></param>
    public record CreateDocumentCommand<T>(T Document) : IRequest<T>;
}
