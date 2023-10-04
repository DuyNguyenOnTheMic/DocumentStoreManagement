using DocumentStoreManagement.Core.Models.MongoDB;
using MediatR;

namespace DocumentStoreManagement.Services.Commands
{
    /// <summary>
    /// Command class to update document
    /// </summary>
    public class UpdateDocumentCommand : IRequest
    {
        public string Id { get; set; }
        public string PublisherName { get; set; } = null!;
        public int ReleaseQuantity { get; set; }
        public Book Book { get; set; }
        public Magazine Magazine { get; set; }
        public Newspaper Newspaper { get; set; }

        public UpdateDocumentCommand(string id, string publisherName, int releaseQuantity, Book book, Magazine magazine, Newspaper newspaper)
        {
            Id = id;
            PublisherName = publisherName;
            ReleaseQuantity = releaseQuantity;
            Book = book;
            Magazine = magazine;
            Newspaper = newspaper;
        }
    }
}
