namespace Nest.Services.CatalogApi.Models.Base;

public abstract class NameAuditiableEntity : BaseEntity
{
    public string Name { get; set; } = null!;
}
