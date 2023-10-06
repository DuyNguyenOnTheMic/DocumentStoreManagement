namespace DocumentStoreManagement.Core.Models.MongoDB
{
    /// <summary>
    /// Manazine collection - a document type
    /// </summary>
    public class Magazine : Document
    {
        public int ReleaseNumber { get; set; }
        public string ReleaseMonth { get; set; } = null!;
    }
}
