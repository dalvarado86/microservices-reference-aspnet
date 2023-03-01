using AutoMapper;
using MediatR;
using Orders.Application.Abstractions;
using Orders.Application.Models;
using Orders.Application.Orders.Queries;

namespace Orders.Application.Orders.Handlers
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrderDto>>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
        }

        public async Task<List<OrderDto>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetOrderByUserNameAsync(request.UserName);
            var result = mapper.Map<List<OrderDto>>(orders);

            return result;
        }
    }
}
