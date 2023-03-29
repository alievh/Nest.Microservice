using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Nest.Services.CatalogApi.Models.Base;

namespace Nest.Services.CatalogApi.Models;

public class Product : NameAuditiableEntity
{
    public string Description { get; set; } = null!;
    public int Stock { get; set; }

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }

    public string UserId { get; set; } = null!;
    public List<string> Pictures { get; set; } = null!;

    public List<SubCategory>? SubCategories { get; set; }
}
