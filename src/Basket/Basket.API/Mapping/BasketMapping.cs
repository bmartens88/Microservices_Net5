using AutoMapper;
using src.Basket.Basket.API.Entities;
using src.Common.EventBusRabbitMQ.Events;

namespace src.Basket.Basket.API.Mapping
{
  public class BasketMapping : Profile
  {
    public BasketMapping()
    {
      CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
    }
  }
}