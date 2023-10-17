using DocumentStoreManagement.Core;
using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Queries.DocumentQueries;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.DocumentHandlers
{
    public class GetDocumentListByTypeHandler : IRequestHandler<GetDocumentListByTypeQuery, IEnumerable<Document>>
    {
        private readonly IQueryRepository<Book> _bookRepository;
        private readonly IQueryRepository<Magazine> _magazineRepository;
        private readonly IQueryRepository<Newspaper> _newspaperRepository;

        public GetDocumentListByTypeHandler(IQueryRepository<Book> bookRepository, IQueryRepository<Magazine> magazineRepository, IQueryRepository<Newspaper> newspaperRepository)
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
                // Return books
                return await _bookRepository.GetAllAsync();
            }
            else if (type == CustomConstants.DocumentMagazineType)
            {
                // Return magazines
                return await _magazineRepository.GetAllAsync();
            }
            else if (type == CustomConstants.DocumentNewsPaperType)
            {
                // Return newspaper
                return await _newspaperRepository.GetAllAsync();
            }

            // Throw error
            throw new Exception("The input type is not valid, please try again!");
        }
    }
}
