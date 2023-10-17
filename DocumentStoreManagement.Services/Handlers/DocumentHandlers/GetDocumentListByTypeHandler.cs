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
            // Declare variables
            int type = query.Type;
            string function = "get_all_documents()";

            // Return class model based on input type
            if (type == CustomConstants.DocumentBookType)
            {
                // Return books
                return await _bookRepository.GetAllAsync(function);
            }
            if (type == CustomConstants.DocumentMagazineType)
            {
                // Return magazines
                return await _magazineRepository.GetAllAsync(function);
            }
            if (type == CustomConstants.DocumentNewsPaperType)
            {
                // Return newspaper
                return await _newspaperRepository.GetAllAsync(function);
            }

            // Throw error
            throw new Exception("The input type is not valid, please try again!");
        }
    }
}
