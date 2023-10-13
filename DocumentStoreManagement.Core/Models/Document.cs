using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentStoreManagement.Core.Models
{
    /// <summary>
    /// Document Collection to manage documents
    /// </summary>
    [BsonKnownTypes(typeof(Book), typeof(Magazine), typeof(Newspaper))]
    public class Document : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string PublisherName { get; set; }
        [Required]
        public int ReleaseQuantity { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal UnitPrice { get; set; }
    }
}
