using MediatR;
using Orders.Application.Models;

namespace Orders.Application.Orders.Commands
{
    public class UpdateOrderCommand : IRequest<Unit>
    {
        public UpdateOrderCommand(OrderDto order)
        {
            this.Order = order ?? throw new ArgumentNullException(nameof(order));
        }

        public int Id { get; set; }
        public OrderDto Order { get; set; }
    }
}
