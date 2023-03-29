using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Nest.Services.CatalogApi.Models.Base;

namespace Nest.Services.CatalogApi.Models;

public class SubCategory : NameAuditiableEntity
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string CategoryId { get; set; } = null!;
    public Category? Category { get; set; }
}
