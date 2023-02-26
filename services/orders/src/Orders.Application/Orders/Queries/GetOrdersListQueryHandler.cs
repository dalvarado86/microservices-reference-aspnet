using AutoMapper;
using MediatR;
using Orders.Application.Abstractions;
using Orders.Application.Models;

namespace Orders.Application.Orders.Queries
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
            var orders = await this.orderRepository.GetOrderByUserNameAsync(request.UserName);
            var result = this.mapper.Map<List<OrderDto>>(orders);

            return result;
        }
    }
}
