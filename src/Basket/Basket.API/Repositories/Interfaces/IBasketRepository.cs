using System.Threading.Tasks;
using src.Basket.Basket.API.Entities;

namespace src.Basket.Basket.API.Repositories.Interfaces
{
  public interface IBasketRepository
  {
    Task<BasketCart> GetBasket(string userName);
    Task<BasketCart> UpdateBasket(BasketCart basket);
    Task<bool> DeleteBasket(string userName);
  }
}