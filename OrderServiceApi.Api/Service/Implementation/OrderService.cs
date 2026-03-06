using OrderServiceApi.Api.Data.Dtos.Requests;
using OrderServiceApi.Api.Data.Dtos.Responses;
using OrderServiceApi.Api.Data.Repository.Interface;
using OrderServiceApi.Api.Service.Interface;

namespace OrderServiceApi.Api.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IdRepository _idRepository;

        public OrderService (ILogger<OrderService> logger, IdRepository repository)
        {
            _logger = logger;
            _idRepository = repository;
        }

        #region Cancel Order
        public async Task<OrderCancelledResponse> CancleOrder(CancelOrder order)
        {
            try
            {
                var result = await _idRepository.CancelOrderAsync(order);
                return new OrderCancelledResponse
                {
                    ResponseCode = result > 0 ? "00" : "01",
                    ResponseMessage = result > 0 ? "Order cancelled successfully." : "Failed to cancel order.",
                    Details = null!
                };
            }
            catch
            {
                _logger.LogError($"Error cancelling order with ID, {order.orderId}");
                return new OrderCancelledResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "An error occurred while cancelling the order. Please try again later.",
                    Details = null!
                };
            }
        }
        #endregion

        #region Create Order
        public async Task<OrderCreationResponse> CreateOrder(CreateOrderRequest order)
        {
            try
            {
                order.orderId = Guid.NewGuid().ToString();
                var response = await _idRepository.CreateOrderAsync(order);
                return response;
            }
            catch
            {
                _logger.LogError($"Error creating order with ID, {order.orderId}");
                return new OrderCreationResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "An error occurred while creating the order. Please try again later.",
                    Details = null!
                };
            }
        }
        #endregion
    }
}