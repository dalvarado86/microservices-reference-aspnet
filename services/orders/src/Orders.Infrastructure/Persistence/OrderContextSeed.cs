using Microsoft.Extensions.Logging;
using Orders.Domain.Entities;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation($"Seed database associated with context {typeof(OrderContext).Name}");
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order()
                {
                    UserName = "david",
                    FirstName = "David",
                    LastName = "Alvarado",
                    EmailAddress = "dealvarado86@gmail.com",
                    AddressLine = "Austin, Tx",
                    Country = "US",
                    TotalPrice = 350
                }
            };
        }
    }
}