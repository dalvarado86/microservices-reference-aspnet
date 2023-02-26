using Orders.Domain.Entities;

namespace Orders.Application.Abstractions
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrderByUserNameAsync(string userName);
    }
}
