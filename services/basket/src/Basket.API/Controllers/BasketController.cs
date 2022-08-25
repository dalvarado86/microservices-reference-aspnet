using Basket.API.Entities;
using Basket.API.Repositories;
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

        public BasketController(
            IBasketRepository basketRepository,
            ILogger<BasketController> logger)
        {
            this.basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
            this.logger.LogInformation("Updating/Creating shopping cart.", basket);
            var response = await this.basketRepository.UpdateBasketAsync(basket);

            this.logger.LogInformation("Shopping cart updated/created.", response);
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
