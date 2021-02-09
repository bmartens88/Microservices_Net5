using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Basket.Basket.API.Entities;
using src.Basket.Basket.API.Repositories.Interfaces;

namespace src.Basket.Basket.API.Controllers
{
  [ApiController]
  [Route("api/v1/[controller]")]
  public class BasketController : ControllerBase
  {
    private readonly IBasketRepository _repository;

    public BasketController(IBasketRepository repository)
    {
      _repository = repository ??
        throw new ArgumentNullException();
    }

    [HttpGet("{userName}")]
    [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<BasketCart>> GetBasket(string userName)
    {
      var basket = await _repository.GetBasket(userName);
      return Ok(basket);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<BasketCart>> UpdateBasket([FromBody] BasketCart basket)
    {
      return Ok(await _repository.UpdateBasket(basket));
    }

    [HttpDelete("{userName}")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<BasketCart>> DeleteBasket(string userName)
    {
      return Ok(await _repository.DeleteBasket(userName));
    }
  }
}