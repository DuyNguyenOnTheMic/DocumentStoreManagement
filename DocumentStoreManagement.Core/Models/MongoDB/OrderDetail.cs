using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DocumentStoreManagement.Models.MongoDB
{
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
