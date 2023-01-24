using Basket.API.Entities;
using Basket.API.Repositories;
using Basket.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly ILogger<BasketController> logger;
        private readonly IBasketRepository basketRepository;
        private readonly DiscountGrpcService discountGrpcService;

        public BasketController(
            ILogger<BasketController> logger,
            IBasketRepository basketRepository,
            DiscountGrpcService discountGrpcService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            this.discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
        }

        [HttpGet("{username}", Name = "GetBasketAsync")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasketAsync(string userName)
        {
            this.logger.LogInformation("Looking for shopping cart by username.", new { UserName = userName });

            var basket = await this.basketRepository.GetBasketAsync(userName);
            var response = basket ?? new ShoppingCart(userName);

            this.logger.LogInformation("Shopping cart retrived.", response);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasketAsync([FromBody] ShoppingCart basket)
        {
            this.logger.LogInformation("Updating or creating shopping cart.", basket);

            foreach (var item in basket.Items)
            {
                var coupon = await this.discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }

            var response = await this.basketRepository.UpdateBasketAsync(basket);

            this.logger.LogInformation("Shopping cart has been updated or created.", response);
            return Ok(response);
        }

        [HttpDelete("{userName}", Name = "DeleteBasketAsync")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            this.logger.LogInformation("Deleting shopping cart by username.", new { UserName = userName });
            await this.basketRepository.DeleteBasketAsync(userName);

            this.logger.LogInformation("Shopping cart deleted.", new { UserName = userName });
            return Ok();
        }
    }
}
