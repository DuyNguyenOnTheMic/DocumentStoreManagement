using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models.MongoDB;
using DocumentStoreManagement.Services.Commands;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers
{
    public class DeleteDocumentHandler : IRequestHandler<DeleteDocumentCommand>
    {
        private readonly IGenericRepository<Document> _documentRepository;

        public DeleteDocumentHandler(IGenericRepository<Document> documentRepository)
        {
            _documentRepository = documentRepository;
        }

        /// <summary>
        /// Handler to delete document
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(DeleteDocumentCommand command, CancellationToken cancellationToken)
        {
            var document = await _documentRepository.GetByIdAsync(command.Id);
            if (document == null)
            {
                var cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.Cancel();
            }

            await _documentRepository.RemoveAsync(document);
        }
    }
}
