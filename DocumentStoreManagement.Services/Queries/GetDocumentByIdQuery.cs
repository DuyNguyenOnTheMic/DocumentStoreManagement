using DocumentStoreManagement.Core.Models.MongoDB;
using MediatR;

namespace DocumentStoreManagement.Services.Queries
{
    public class GetDocumentByIdQuery : IRequest<Document>
    {
        public string Id { get; set; }
    }
}
