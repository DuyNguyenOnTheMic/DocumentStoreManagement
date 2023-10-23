using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Commands.DocumentCommands;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.DocumentHandlers
{
    public class CreateDocumentHandler<T>(IRepository<T> documentRepository) : IRequestHandler<CreateDocumentCommand<T>, T> where T : BaseEntity
    {
        private readonly IRepository<T> _documentRepository = documentRepository;

        /// <summary>
        /// Handler to create new document
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        public async Task<T> Handle(CreateDocumentCommand<T> command, CancellationToken cancellationToken)
        {
            await _documentRepository.AddAsync(command.Document);
            return command.Document;
        }
    }
}
