using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Queries.DocumentQueries;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.DocumentHandlers
{
    public class GetDocumentByIdHandler : IRequestHandler<GetDocumentByIdQuery, Document>
    {
        private readonly IGenericRepository<Book> _bookRepository;
        private readonly IGenericRepository<Magazine> _magazineRepository;
        private readonly IGenericRepository<Newspaper> _newspaperRepository;

        public GetDocumentByIdHandler(IGenericRepository<Book> bookRepository, IGenericRepository<Magazine> magazineRepository, IGenericRepository<Newspaper> newspaperRepository)
        {
            _bookRepository = bookRepository;
            _magazineRepository = magazineRepository;
            _newspaperRepository = newspaperRepository;
        }

        /// <summary>
        /// Hanlder to find document by id
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        public async Task<Document> Handle(GetDocumentByIdQuery query, CancellationToken cancellationToken)
        {
            string id = query.Id;

            // Find in book table
            Book book = await _bookRepository.GetByIdAsync(id);
            if (book != null)
            {
                return book;
            }

            // Find in magazine table
            Magazine magazine = await _magazineRepository.GetByIdAsync(id);
            if (magazine != null)
            {
                return magazine;
            }

            // Find in newspaper table
            Newspaper newspaper = await _newspaperRepository.GetByIdAsync(id);
            if (newspaper != null)
            {
                return newspaper;
            }

            throw new Exception("No document found 😔");
        }
    }
}
