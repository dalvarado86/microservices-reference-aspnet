using AutoMapper;
using Orders.Application.Models;
using Orders.Domain.Entities;

namespace Orders.Application.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderVm>().ReverseMap();
        }
    }
}
