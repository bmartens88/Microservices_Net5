namespace src.Catalog.Catalog.API.Entities.Settings
{
  public interface ICatalogDatabaseSettings
  {
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
    string CollectionName { get; set; }
  }
}