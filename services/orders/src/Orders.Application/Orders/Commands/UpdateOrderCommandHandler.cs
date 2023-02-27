using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Orders.Application.Abstractions;

namespace Orders.Application.Orders.Commands
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly ILogger<UpdateOrderCommandHandler> logger;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderRequest = await this.orderRepository.GetByIdAsync(request.Id);

            if (orderRequest == null)
            {
                throw new InvalidOperationException($"Order {request.Id} doesn't exist.");
            }

            this.mapper.Map(request.Order, orderRequest);

            await this.orderRepository.UpdateAsync(orderRequest);

            this.logger.LogInformation($"Order {orderRequest.Id} is successfully updated.");

            return Unit.Value;
        }
    }
}
