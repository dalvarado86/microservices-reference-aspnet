using MediatR;
using Orders.Application.Models;

namespace Orders.Application.Orders.Queries
{
    public class GetOrdersListQuery: IRequest<List<OrderVm>>
    {
        public string UserName { get; set; }

        public GetOrdersListQuery(string userName)
        {
            this.UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }

    }
}
