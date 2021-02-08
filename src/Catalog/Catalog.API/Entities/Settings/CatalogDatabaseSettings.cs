namespace src.Catalog.Catalog.API.Entities.Settings
{
  public class CatalogDatabaseSettings : ICatalogDatabaseSettings
  {
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }
  }
}