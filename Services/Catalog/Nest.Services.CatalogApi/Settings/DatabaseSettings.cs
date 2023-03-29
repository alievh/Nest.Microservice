namespace Nest.Services.CatalogApi.Settings;

public class DatabaseSettings : IDatabaseSettings
{
    public string DatabaseName { get; set; } = null!;
    public string ConnectionString { get; set; } = null!;
    public string CourseCollectionName { get; set; } = null!;
    public string CategoryCollectionName { get; set; } = null!;
    public string SubCategoryCollectionName { get; set; } = null!;
}
