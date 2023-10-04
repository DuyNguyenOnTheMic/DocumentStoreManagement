using DocumentStoreManagement.Core.Models.MongoDB;
using MediatR;

namespace DocumentStoreManagement.Services.Queries
{
    /// <summary>
    /// Query class to get all documents
    /// </summary>
    public record GetDocumentListQuery() : IRequest<IEnumerable<Document>>;
}
