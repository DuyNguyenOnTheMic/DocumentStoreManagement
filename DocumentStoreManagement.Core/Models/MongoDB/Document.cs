namespace DocumentStoreManagement.Core.Models.MongoDB
{
    /// <summary>
    /// Document Collection to manage documents
    /// </summary>
    public class Document : BaseEntity
    {
        public string PublisherName { get; set; } = null!;
        public int ReleaseQuantity { get; set; }
        public Book Book { get; set; }
        public Magazine Magazine { get; set; }
        public Newspaper Newspaper { get; set; }
    }
}
