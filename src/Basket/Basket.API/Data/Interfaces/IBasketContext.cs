using StackExchange.Redis;

namespace src.Basket.Basket.API.Data.Interfaces
{
  public interface IBasketContext
  {
    IDatabase Redis { get; }
  }
}