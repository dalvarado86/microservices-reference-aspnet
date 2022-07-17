using Ardalis.GuardClauses;
using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Repositories
{
    // TODO: Add logger

    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache distributedCache;

        public BasketRepository(IDistributedCache distributedCache)
        {
            this.distributedCache = Guard.Against.Null(distributedCache, nameof(distributedCache));
        }

        public async Task DeleteBasketAsync(string userName)
        {
            await this.distributedCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart?> GetBasketAsync(string userName)
        {
            var basket = await this.distributedCache.GetStringAsync(userName);

            if (basket is not null)
            {
                return JsonSerializer.Deserialize<ShoppingCart>(basket);
            }

            return null;
        }

        public async Task<ShoppingCart?> UpdateBasketAsync(ShoppingCart basket)
        {
            Validate(basket);

            var value = JsonSerializer.Serialize(basket);
            await this.distributedCache.SetStringAsync(basket.UserName, value);

            return await GetBasketAsync(basket.UserName);
        }

        private static void Validate(ShoppingCart basket)
        {
            if (basket is null)
            {
                throw new ArgumentNullException(nameof(basket));
            }

            if (basket.UserName is null)
            {
                throw new ArgumentException($"{nameof(basket.UserName)} cannot be empty.");
            }
        }
    }
}
