namespace DocumentStoreManagement.Core.Models.MongoDB
{
    public class Document : BaseEntity
    {
        /// <summary>
        /// Document collection
        /// </summary>
        public string PublisherName { get; set; } = null!;
        public int ReleaseQuantity { get; set; }
    }
}
