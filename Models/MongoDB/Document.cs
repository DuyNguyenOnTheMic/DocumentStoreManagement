using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DocumentStoreManagement.Models.MongoDB
{
    public class Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string PublisherName { get; set; } = null!;
        public int ReleaseQuantity { get; set; }
    }
}
