namespace DocumentStoreManagement.Core.Models.MongoDB
{
    /// <summary>
    /// Newspaper collection - a document type
    /// </summary>
    public class Newspaper : Document
    {
        public DateTime ReleaseDate { get; set; }
    }
}
