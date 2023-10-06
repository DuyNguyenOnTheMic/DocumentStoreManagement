using System.ComponentModel.DataAnnotations;

namespace DocumentStoreManagement.Core.Models
{
    /// <summary>
    /// Document Collection to manage documents
    /// </summary>
    public class Document : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string PublisherName { get; set; }
        [Required]
        public int ReleaseQuantity { get; set; }
    }
}
