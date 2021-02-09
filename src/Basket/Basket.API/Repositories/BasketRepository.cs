using System;
using System.Text.Json;
using System.Threading.Tasks;
using src.Basket.Basket.API.Data.Interfaces;
using src.Basket.Basket.API.Entities;
using src.Basket.Basket.API.Repositories.Interfaces;

namespace src.Basket.Basket.API.Repositories
{
  public class BasketRepository : IBasketRepository
  {
    private readonly IBasketContext _context;

    public BasketRepository(IBasketContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<BasketCart> GetBasket(string userName)
    {
      var basket = await _context.Redis.StringGetAsync(userName);

      if (basket.IsNullOrEmpty)
      {
        return null;
      }

      return JsonSerializer.Deserialize<BasketCart>(basket);
    }

    public async Task<BasketCart> UpdateBasket(BasketCart basket)
    {
      var updated = await _context.Redis.StringSetAsync(basket.UserName, JsonSerializer.Serialize(basket));

      if (!updated) return null;

      return await GetBasket(basket.UserName);
    }

    public async Task<bool> DeleteBasket(string userName)
    {
      return await _context.Redis.KeyDeleteAsync(userName);
    }
  }
}