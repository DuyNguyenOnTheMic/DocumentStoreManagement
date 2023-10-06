using System.ComponentModel.DataAnnotations;

namespace DocumentStoreManagement.Core.Models.PostgresQL
{
    public class Document
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string PublisherName { get; set; }
        [Required]
        public int ReleaseQuantity { get; set; }
    }
}
