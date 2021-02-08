using MongoDB.Driver;
using src.Catalog.Catalog.API.Entities;
using src.Catalog.Catalog.API.Entities.Settings;

namespace src.Catalog.Catalog.API.Data
{
  public class CatalogContext : ICatalogContext
  {
    public CatalogContext(ICatalogDatabaseSettings settings)
    {
      var client = new MongoClient(settings.ConnectionString);
      var database = client.GetDatabase(settings.DatabaseName);

      Products = database.GetCollection<Product>(settings.CollectionName);
      CatalogContextSeed.SeedData(Products);
    }

    public IMongoCollection<Product> Products { get; }
  }
}