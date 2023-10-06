using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DocumentStoreManagement.Core.Models.MongoDB
{
    /// <summary>
    /// A non-instantiable base entity which defines members available across all entities
    /// </summary>
    public abstract class BaseEntity
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
