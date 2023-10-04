using DocumentStoreManagement.Core.Models.MongoDB;
using MediatR;

namespace DocumentStoreManagement.Services.Queries.DocumentQueries
{
    /// <summary>
    /// Query class to find document by id
    /// </summary>
    public class GetDocumentByIdQuery : IRequest<Document>
    {
        public string Id { get; set; }
    }
}
