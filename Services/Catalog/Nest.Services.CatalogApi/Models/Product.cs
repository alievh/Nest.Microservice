using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Nest.Services.CatalogApi.Models.Base;

namespace Nest.Services.CatalogApi.Models;

public class Product : NameAuditiableEntity
{
    public string Description { get; set; } = null!;
    public int Stock { get; set; }
    public int? Sold { get; set; }

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }

    public string UserId { get; set; } = null!;
    public List<string> Pictures { get; set; } = null!;

    [BsonRepresentation(BsonType.ObjectId)]
    public string SubCategoryId { get; set; } = null!;

    [BsonIgnore]
    public SubCategory? SubCategory { get; set; }
}
