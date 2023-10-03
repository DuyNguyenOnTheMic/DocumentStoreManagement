using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DocumentStoreManagement.Models.MongoDB
{
    /// <summary>
    /// A non-instantiable base entity which defines members available across all entities
    /// </summary>
    public abstract class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
