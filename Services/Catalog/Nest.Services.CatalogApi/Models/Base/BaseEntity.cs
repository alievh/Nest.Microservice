using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Nest.Services.CatalogApi.Models.Base;

public abstract class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedTime { get; set; }
}
