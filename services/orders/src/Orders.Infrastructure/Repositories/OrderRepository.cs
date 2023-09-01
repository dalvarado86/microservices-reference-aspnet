using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure.Persistence;
using Orders.Application.Abstractions;
using Orders.Domain.Entities;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Order>> GetOrderByUserNameAsync(string userName)
        {
            var orderList = await this.dbContext.Orders
                .Where(o => o.UserName == userName)
                .ToListAsync();

            return orderList;
        }
    }
}