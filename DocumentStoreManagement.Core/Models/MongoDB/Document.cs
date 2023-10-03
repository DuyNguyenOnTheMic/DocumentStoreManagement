namespace DocumentStoreManagement.Models.MongoDB
{
    public class Document : BaseEntity
    {
        public string PublisherName { get; set; } = null!;
        public int ReleaseQuantity { get; set; }
    }
}
