﻿namespace Nest.Services.CatalogApi.Settings;

public interface IDatabaseSettings
{
    public string DatabaseName { get; set; }
    public string ConnectionString { get; set; }
    public string CourseCollectionName { get; set; }
    public string CategoryCollectionName { get; set; }
    public string SubCategoryCollectionName { get; set; }
}
