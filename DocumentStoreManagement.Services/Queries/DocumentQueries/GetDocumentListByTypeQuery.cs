using DocumentStoreManagement.Core.Models.MongoDB;
using MediatR;

namespace DocumentStoreManagement.Services.Queries.DocumentQueries
{
    /// <summary>
    /// Query class to get documents by type
    /// </summary>
    public record GetDocumentListByTypeQuery(string Type) : IRequest<IEnumerable<Document>>;
}
