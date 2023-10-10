using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentStoreManagement.Core.Models
{
    /// <summary>
    /// Book collection - a document type
    /// </summary>
    [Table("Books")]
    public class Book : Document
    {
        [Required]
        [MaxLength(255)]
        public string AuthorName { get; set; }
        [Required]
        public int PageNumber { get; set; }
    }
}
