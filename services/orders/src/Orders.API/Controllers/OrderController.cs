﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.Models;
using Orders.Application.Orders.Commands;
using Orders.Application.Orders.Queries;
using System.Net;

namespace Orders.API.Controllers
{
    //[ApiController]
    //[Route("api/v1/[controller]")]
    /// <summary>
    /// This controller is deprecated. Use the <see cref="Endpoints.OrderEndpoints"/> instead.
    /// </summary>
    [Obsolete("This class is deprecated. Use the <see cref=\"Endpoints.OrderEndpoints\"/> instead.")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator mediator;
        

        public OrderController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{userName}", Name = "GetOrder")]
        [ProducesResponseType(typeof(IEnumerable<OrderVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderVm>>> GetOrdersByUserName(string userName)
        {
            var query = new GetOrdersListQuery(userName);
            var orders = await this.mediator.Send(query);
            return Ok(orders);
        }

        // for testing purpose
        [HttpPost(Name = "CheckoutOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
        {
            var result = await this.mediator.Send(command);
            return Ok(result);
        }

        [HttpPut(Name = "UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
            await this.mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var command = new DeleteOrderCommand() { Id = id };
            await this.mediator.Send(command);
            return NoContent();
        }
    }
}
