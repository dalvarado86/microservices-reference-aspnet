using Basket.API.Entities;
using Basket.API.Repositories;
using Commons.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    public class BasketController : BaseApiController
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

        [HttpGet("{username}")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasketAsync(string userName)
        {
            this.logger.LogInformation($"Looking for shopping cart by username: {userName}");
            var basket = await this.basketRepository.GetBasketAsync(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasketAsync([FromBody] ShoppingCart basket)
        {
            this.logger.LogInformation($"Updating shopping cart: {basket}");
            return Ok(await this.basketRepository.UpdateBasketAsync(basket));
        }

        [HttpDelete("{userName}", Name = "DeleteBasketAsync")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            this.logger.LogInformation($"Deleting shopping cart by username: {userName}");
            await this.basketRepository.DeleteBasketAsync(userName);
            return Ok();
        }
    }
}
