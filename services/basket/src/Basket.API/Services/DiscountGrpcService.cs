using Discount.Grpc.Protos;

namespace Basket.API.Services
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        {
            this.discountProtoServiceClient = discountProtoServiceClient ?? throw new ArgumentNullException(nameof(discountProtoServiceClient));
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(productName, nameof(productName));

            var discountRequest = new GetDiscountRequest { ProductName = productName };

            return await this.discountProtoServiceClient.GetDiscountAsync(discountRequest);
        }
    }
}
