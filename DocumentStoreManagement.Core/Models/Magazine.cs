using System.ComponentModel.DataAnnotations;

namespace DocumentStoreManagement.Core.Models
{
    /// <summary>
    /// Manazine collection - a document type
    /// </summary>
    public class Magazine : Document
    {
        [Required]
        public int ReleaseNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string ReleaseMonth { get; set; }
    }
}
