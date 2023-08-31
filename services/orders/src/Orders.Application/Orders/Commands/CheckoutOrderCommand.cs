using MediatR;
using Orders.Application.Models;

namespace Orders.Application.Orders.Commands
{
    public class CheckoutOrderCommand : IRequest<int>
    {
        public CheckoutOrderCommand(OrderVm order)
        {
            this.Order = order ?? throw new ArgumentNullException(nameof(order));
        }

        public OrderVm Order { get; set; }
    }
}
