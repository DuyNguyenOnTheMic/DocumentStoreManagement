using DocumentStoreManagement.Core;
using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Queries.DocumentQueries;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.DocumentHandlers
{
    public class GetDocumentListByTypeHandler : IRequestHandler<GetDocumentListByTypeQuery, IEnumerable<Document>>
    {
        private readonly IGenericRepository<Book> _bookRepository;
        private readonly IGenericRepository<Magazine> _magazineRepository;
        private readonly IGenericRepository<Newspaper> _newspaperRepository;

        public GetDocumentListByTypeHandler(IGenericRepository<Book> bookRepository, IGenericRepository<Magazine> magazineRepository, IGenericRepository<Newspaper> newspaperRepository)
        {
            _bookRepository = bookRepository;
            _magazineRepository = magazineRepository;
            _newspaperRepository = newspaperRepository;
        }

        /// <summary>
        /// Handler to get documents by type
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        public async Task<IEnumerable<Document>> Handle(GetDocumentListByTypeQuery query, CancellationToken cancellationToken)
        {
            int type = query.Type;

            // Return class model based on input type
            if (type == CustomConstants.DocumentBookType)
            {
                return await _bookRepository.GetAllAsync();
            }
            else if (type == CustomConstants.DocumentMagazineType)
            {
                return await _magazineRepository.GetAllAsync();
            }
            else if (type == CustomConstants.DocumentNewsPaperType)
            {
                return await _newspaperRepository.GetAllAsync();
            }

            throw new Exception();
        }
    }
}
