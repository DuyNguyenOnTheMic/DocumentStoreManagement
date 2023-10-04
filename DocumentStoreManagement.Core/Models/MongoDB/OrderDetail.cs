using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DocumentStoreManagement.Core.Models.MongoDB
{
    /// <summary>
    /// OrderDetail collection - to store order details of customer order
    /// </summary>
    public class OrderDetail
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public string DocumentId { get; set; } = null!;
    }
}
