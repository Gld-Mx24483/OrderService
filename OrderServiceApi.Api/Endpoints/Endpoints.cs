using OrderServiceApi.Api.Data.Dtos.Requests;
using OrderServiceApi.Api.Data.Dtos.Responses;
using OrderServiceApi.Api.Service.Interface;

namespace OrderServiceApi.Api.Endpoints
{
    public static class Endpoints
    {
        public const string BaseUrl = "/taima-channel/api/v1/orders";
        public static void OrdersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGroup(BaseUrl)
            .MapPost("/create-order", async (CreateOrderRequest request, IOrderService orderService) =>
            {
                var response = await orderService.CreateOrder(request);
                return ResponseMapper.MapResponse(response.ResponseCode!, response);
            }).Produces<OrderCreationResponse>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest)
              .WithName("CreateOrder")
              .WithTags("Orders");

            app.MapGroup(BaseUrl)
            .MapPost("/cancel-order", async (CancelOrder request, IOrderService orderService) =>
            {
                var response = await orderService.CancleOrder(request);
                return ResponseMapper.MapResponse(response.ResponseCode!, response);
            }).Produces<OrderCancelledResponse>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest)
              .WithName("CancelOrder")
              .WithTags("Orders");
        }
    }
}