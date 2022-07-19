using AutoMapper;
using Discount.Common.Entities;
using Discount.Grpc.Protos;

namespace Discount.Grpc.Profiles
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}
