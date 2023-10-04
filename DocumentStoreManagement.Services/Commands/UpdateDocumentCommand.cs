using DocumentStoreManagement.Core.Models.MongoDB;
using MediatR;

namespace DocumentStoreManagement.Services.Commands
{
    public class UpdateDocumentCommand : IRequest<int>
    {
        public string Id { get; set; }
        public string PublisherName { get; set; } = null!;
        public int ReleaseQuantity { get; set; }
        public Book Book { get; set; }
        public Magazine Magazine { get; set; }
        public Newspaper Newspaper { get; set; }

        public UpdateDocumentCommand(string publisherName, int releaseQuantity, Book book, Magazine magazine, Newspaper newspaper)
        {
            PublisherName = publisherName;
            ReleaseQuantity = releaseQuantity;
            Book = book;
            Magazine = magazine;
            Newspaper = newspaper;
        }
    }
}
