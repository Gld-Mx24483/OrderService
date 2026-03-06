using OrderServiceApi.Api.Data.Dtos.Requests;
using OrderServiceApi.Api.Data.Dtos.Responses;

namespace OrderServiceApi.Api.Service.Interface
{
    public interface IOrderService
    {
        Task<OrderCreationResponse> CreateOrder(CreateOrderRequest order);
        Task<OrderCancelledResponse> CancleOrder(CancelOrder order);
    }
}