using OrderServiceApi.Api.Data.Dtos.Requests;
using OrderServiceApi.Api.Data.Dtos.Responses;

namespace OrderServiceApi.Api.Data.Repository.Interface
{
    public interface IdRepository
    {
        Task<OrderCreationResponse> CreateOrderAsync(CreateOrderRequest order);
        Task<int> OrderShippedResponseAsync(ShipOrderRequest order);
        Task<int> CancelOrderAsync(CancelOrder order);
    }
}