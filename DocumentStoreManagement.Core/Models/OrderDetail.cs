using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentStoreManagement.Core.Models
{
    /// <summary>
    /// OrderDetail collection - to store order details of customer order
    /// </summary>
    public class OrderDetail
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Total { get; set; }
        [Required]
        [ForeignKey("Document")]
        public string DocumentId { get; set; }
        public Document Document { get; set; }
        [Required]
        [ForeignKey("Order")]
        public string OrderId { get; set; }
        public Order Order { get; set; }
    }
}
