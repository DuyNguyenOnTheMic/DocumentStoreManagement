namespace DocumentStoreManagement.Models.MongoDB
{
    public class Magazine : Document
    {
        public int ReleaseNumber { get; set; }
        public string ReleaseMonth { get; set; } = null!;
    }
}
