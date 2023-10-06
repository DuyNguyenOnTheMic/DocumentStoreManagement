namespace DocumentStoreManagement.Core.Models.MongoDB
{
    /// <summary>
    /// Book collection - a document type
    /// </summary>
    public class Book : Document
    {
        public string AuthorName { get; set; } = null!;
        public int PageNumber { get; set; }
    }
}
