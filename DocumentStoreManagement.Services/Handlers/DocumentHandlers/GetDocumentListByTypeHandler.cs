using DocumentStoreManagement.Core;
using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Queries.DocumentQueries;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.DocumentHandlers
{
    public class GetDocumentListByTypeHandler(IQueryRepository<Book> bookRepository, IQueryRepository<Magazine> magazineRepository, IQueryRepository<Newspaper> newspaperRepository) : IRequestHandler<GetDocumentListByTypeQuery, IEnumerable<Document>>
    {
        private readonly IQueryRepository<Book> _bookRepository = bookRepository;
        private readonly IQueryRepository<Magazine> _magazineRepository = magazineRepository;
        private readonly IQueryRepository<Newspaper> _newspaperRepository = newspaperRepository;

        /// <summary>
        /// Handler to get documents by type
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        public async Task<IEnumerable<Document>> Handle(GetDocumentListByTypeQuery query, CancellationToken cancellationToken)
        {
            // Declare variables
            int type = query.Type;
            string table = CustomConstants.DocumentsTable;

            // Return class model based on input type
            if (type == CustomConstants.DocumentBookType)
            {
                // Return books
                return await _bookRepository.GetAllAsync(table);
            }
            if (type == CustomConstants.DocumentMagazineType)
            {
                // Return magazines
                return await _magazineRepository.GetAllAsync(table);
            }
            if (type == CustomConstants.DocumentNewsPaperType)
            {
                // Return newspaper
                return await _newspaperRepository.GetAllAsync(table);
            }

            // Throw error
            throw new Exception("The input type is not valid, please try again!");
        }
    }
}
