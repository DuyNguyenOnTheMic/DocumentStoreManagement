using System.ComponentModel.DataAnnotations;

namespace DocumentStoreManagement.Core.Models.MongoDB
{
    /// <summary>
    /// Book collection - a document type
    /// </summary>
    public class Book : Document
    {
        [Required]
        [MaxLength(255)]
        public string AuthorName { get; set; }
        [Required]
        public int PageNumber { get; set; }
    }
}
