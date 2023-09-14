using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.API.Models;
using Orders.Application.Orders.Commands;
using Orders.Application.Orders.Queries;
using System.Net;

namespace Orders.API.Endpoints
{
    public static class OrderEndpoints
    {
        // TODO: Adds error handling
        private const string BaseRoute = "api/v1/order";

        public static void ConfigureOrderEndpoints(this WebApplication app)
        {
            app.MapGet("api/v1/order/{userName}", GetOrdersByUserNameAsync)
                .WithName("GetOrder")
                .Produces<IEnumerable<ApiResponse>>((int)HttpStatusCode.OK);

            app.MapPost("api/v1/order", CheckoutOrderAsync)
                .WithName("CheckoutOrder")
                .Accepts<CheckoutOrderCommand>("application/json")
                .Produces<ApiResponse>((int)HttpStatusCode.Created)
                .Produces((int)HttpStatusCode.BadRequest);

            app.MapPut("api/v1/order", UpdateOrderAsync)
                .WithName("UpdateOrder")
                .Accepts<UpdateOrderCommand>("application/json")
                .Produces<ApiResponse>((int)HttpStatusCode.NoContent)
                .Produces((int)HttpStatusCode.BadRequest);

            app.MapDelete("api/v1/order/{id:int}", DeleteOrderAync)
                .WithName("DeleteOrder")
                .Produces((int)HttpStatusCode.NoContent)
                .Produces((int)HttpStatusCode.NotFound);

        }

        private static async Task<IResult> GetOrdersByUserNameAsync(
            string userName,
            IMediator mediator)
        {
            var response = new ApiResponse();
            var query = new GetOrdersListQuery(userName);
            var orders = await mediator.Send(query);
            response.Result = orders;
            response.IsSucess = true;
            response.StatusCode = HttpStatusCode.OK;

            return Results.Ok(response);
        }

        private static async Task<IResult> CheckoutOrderAsync(
            [FromBody] CheckoutOrderCommand command,
            IMediator mediator)
        {
            var response = new ApiResponse() { IsSucess = false, StatusCode = HttpStatusCode.BadRequest };
            var result = await mediator.Send(command);
            command.Order.Id = result;
            response.Result = command.Order;
            response.IsSucess = true;
            response.StatusCode = HttpStatusCode.Created;

            return Results.CreatedAtRoute(
                "GetOrder",
                new { userName = command.Order.UserName }, response);
        }

        private static async Task<IResult> UpdateOrderAsync(
            [FromBody] UpdateOrderCommand command,
            IMediator mediator)
        {
            await mediator.Send(command);
            return Results.NoContent();
        }

        private static async Task<IResult> DeleteOrderAync(
            int id,
            IMediator mediator)
        {
            var command = new DeleteOrderCommand() { Id = id };
            await mediator.Send(command);
            return Results.NoContent();
        }
    }
}
