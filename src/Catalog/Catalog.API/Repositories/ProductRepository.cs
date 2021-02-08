using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using src.Catalog.Catalog.API.Data;
using src.Catalog.Catalog.API.Entities;
using src.Catalog.Catalog.API.Repositories.Interfaces;

namespace src.Catalog.Catalog.API.Repositories
{
  public class ProductRepository : IProductRepository
  {
    private readonly ICatalogContext _context;

    public ProductRepository(ICatalogContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    #region CREATE
    public async Task Create(Product product) =>
      await _context.Products.InsertOneAsync(product);
    #endregion

    #region READ
    public async Task<Product> GetProduct(string id) =>
      await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();

    public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
    {
      FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Category, categoryName);
      FilterDefinition<Product> filter2 = Builders<Product>.Filter.In(p => p.Category, new List<string> { categoryName });

      return await _context.Products.Find(filter2).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByName(string name)
    {
      FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);

      return await _context.Products.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProducts() =>
      await _context.Products.Find(p => true).ToListAsync();
    #endregion

    #region UPDATE
    public async Task<bool> Update(Product product)
    {
      var updateResult = await _context.Products.ReplaceOneAsync(g => g.Id == product.Id, product);

      return updateResult.IsAcknowledged
        && updateResult.ModifiedCount > 0;
    }
    #endregion

    #region DELETE
    public async Task<bool> Delete(string id)
    {
      FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

      var deleteResult = await _context.Products.DeleteOneAsync(filter);

      return deleteResult.IsAcknowledged
        && deleteResult.DeletedCount > 0;
    }
    #endregion
  }
}