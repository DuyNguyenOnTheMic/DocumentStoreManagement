using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentStoreManagement.Core.Models
{
    /// <summary>
    /// Manazine collection - a document type
    /// </summary>
    [Table("Magazines")]
    public class Magazine : Document
    {
        [Required]
        public int ReleaseNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string ReleaseMonth { get; set; }
    }
}
