using MediatR;
using Orders.Application.Models;

namespace Orders.Application.Orders.Commands
{
    public class CheckoutOrderCommand : IRequest<int>
    {
        public CheckoutOrderCommand(OrderDto order)
        {
            this.Order = order ?? throw new ArgumentNullException(nameof(order));
        }

        public OrderDto Order { get; set; }
    }
}
