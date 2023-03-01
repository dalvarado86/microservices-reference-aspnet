using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Orders.Application.Abstractions;
using Orders.Application.Models;
using Orders.Application.Orders.Commands;
using Orders.Domain.Entities;

namespace Orders.Application.Orders.Handlers
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;
        private readonly ILogger<CheckoutOrderCommandHandler> logger;

        public CheckoutOrderCommandHandler(
            IOrderRepository orderRepository,
            IMapper mapper, IEmailService
            emailService,
            ILogger<CheckoutOrderCommandHandler> logger)
        {
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderRequest = mapper.Map<Order>(request.Order);
            var orderResponse = await orderRepository.AddAsync(orderRequest);

            logger.LogInformation($"Order has been create, Id: {orderResponse.Id}");

            await SendMail(orderResponse);

            return orderResponse.Id;
        }

        private async Task SendMail(Order order)
        {
            var email = new Email()
            {
                To = order.EmailAddress,
                Body = $"Microservices Reference: Order {order.Id} was created.",
                Subject = $"Microservices Reference: {order.Id} Order was created"
            };

            try
            {
                await emailService.SendEmailAsync(email);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Send maild failed, {nameof(order.Id)}: {order.Id}.");
            }
        }
    }
}
