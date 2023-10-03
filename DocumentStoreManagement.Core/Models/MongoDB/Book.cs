namespace DocumentStoreManagement.Core.Models.MongoDB
{
    public class Book : Document
    {
        public string AuthorName { get; set; }
        public int PageNumber { get; set; }
    }
}
