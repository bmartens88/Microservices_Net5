using MongoDB.Driver;
using src.Catalog.Catalog.API.Entities;

namespace src.Catalog.Catalog.API.Data
{
  public interface ICatalogContext
  {
    IMongoCollection<Product> Products { get; }
  }
}