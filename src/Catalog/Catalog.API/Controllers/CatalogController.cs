using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using src.Catalog.Catalog.API.Entities;
using src.Catalog.Catalog.API.Repositories.Interfaces;

namespace src.Catalog.Catalog.API.Controllers
{
  [ApiController]
  [Route("api/v1/[controller]")]
  public class CatalogController : ControllerBase
  {
    private readonly IProductRepository _repository;
    private readonly ILogger _logger;

    public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
    {
      _logger = logger;
      _repository = repository ??
        throw new ArgumentNullException(nameof(repository));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
      var products = await _repository.GetProducts();

      return Ok(products);
    }

    [HttpGet("{id:length(24)}", Name = "GetProductById")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> GetProductById(string id)
    {
      var product = await _repository.GetProduct(id);

      if (product == null)
      {
        _logger.LogError($"Product with id: {id}, not found.");
        return NotFound();
      }

      return Ok(product);
    }

    [HttpGet("{categoryName}")]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string categoryName)
    {
      var products = await _repository.GetProductByCategory(categoryName);
      return Ok(products);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
      await _repository.Create(product);
      return CreatedAtRoute("GetProductById", new { id = product.Id }, product);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProduct([FromBody] Product product)
    {
      return Ok(await _repository.Update(product));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProductById(string id)
    {
      return Ok(await _repository.Delete(id));
    }
  }
}