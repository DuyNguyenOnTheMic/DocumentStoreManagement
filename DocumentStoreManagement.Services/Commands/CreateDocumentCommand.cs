using DocumentStoreManagement.Core.Models.MongoDB;
using MediatR;

namespace DocumentStoreManagement.Services.Commands
{
    /// <summary>
    /// Command class to create document
    /// </summary>
    public class CreateDocumentCommand : IRequest<Document>
    {
        public string PublisherName { get; set; } = null!;
        public int ReleaseQuantity { get; set; }
        public Book Book { get; set; }
        public Magazine Magazine { get; set; }
        public Newspaper Newspaper { get; set; }

        public CreateDocumentCommand(string publisherName, int releaseQuantity, Book book, Magazine magazine, Newspaper newspaper)
        {
            PublisherName = publisherName;
            ReleaseQuantity = releaseQuantity;
            Book = book;
            Magazine = magazine;
            Newspaper = newspaper;
        }
    }
}
