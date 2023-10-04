using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DocumentStoreManagement.Core.Models.MongoDB
{
    /// <summary>
    /// Order collection - to store customer orders
    /// </summary>
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int Status { get; set; }
    }
}
