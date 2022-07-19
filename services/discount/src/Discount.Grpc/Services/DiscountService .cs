using AutoMapper;
using Discount.Common.Entities;
using Discount.Common.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository discountRepository;
        private readonly IMapper mapper;
        private readonly ILogger<DiscountService> logger;

        public DiscountService(
            IDiscountRepository discountRepository,
            IMapper mapper,
            ILogger<DiscountService> logger)
        {
            this.discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await this.discountRepository.GetDiscountAsync(request.ProductName);

            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName '{request.ProductName}' is not found."));
            }

            this.logger.LogInformation($"Discount is retrieved for ProductName: {coupon.ProductName}, Amount: {coupon.Amount}");

            var couponModel = this.mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = this.mapper.Map<Coupon>(request.Coupon);

            await this.discountRepository.CreateDiscountAsync(coupon);
            this.logger.LogInformation($"Discount is successfully created. ProductName: {coupon.ProductName}");

            var couponModel = this.mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = this.mapper.Map<Coupon>(request.Coupon);

            await this.discountRepository.UpdateDiscountAsync(coupon);
            this.logger.LogInformation($"Discount is successfully updated. ProductName: {coupon.ProductName}");

            var couponModel = this.mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await this.discountRepository.DeleteDiscountAsync(request.ProductName);

            var response = new DeleteDiscountResponse
            {
                Success = deleted
            };

            this.logger.LogInformation($"Discount is successfully deleted. ProductName: {request.ProductName}");
            return response;
        }
    }

}
