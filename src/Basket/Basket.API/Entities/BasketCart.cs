using System.Collections.Generic;
using System.Linq;

namespace src.Basket.Basket.API.Entities
{
  public class BasketCart
  {
    public string UserName { get; set; }
    public List<BasketCartItem> Items { get; set; } = new List<BasketCartItem>();

    public BasketCart() { }

    public BasketCart(string userName)
    {
      UserName = userName;
    }

    public decimal TotalPrice
    {
      get
      {
        decimal total = 0;
        foreach (var item in Items)
        {
          total += item.Price * item.Quantity;
        }
        return total;
      }
    }
  }
}