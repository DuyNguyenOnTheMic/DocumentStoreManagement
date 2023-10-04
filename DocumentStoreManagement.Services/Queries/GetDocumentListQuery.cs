using DocumentStoreManagement.Core.Models.MongoDB;
using MediatR;

namespace DocumentStoreManagement.Services.Queries
{
    public class GetDocumentListQuery : IRequest<List<Document>>
    {
    }
}
