using AutoMapper;
using Orders.Domain.Entities;

namespace Orders.Application.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            //CreateMap<Order, OrdersVm>().ReverseMap();
            //CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
            //CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        }
    }
}
