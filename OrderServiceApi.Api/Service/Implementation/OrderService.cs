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
        private readonly IEventPublisher _eventPublisher;

        public OrderService (ILogger<OrderService> logger, IdRepository repository, IEventPublisher eventPublisher)
        {
            _logger = logger;
            _idRepository = repository;
            _eventPublisher = eventPublisher;
        }

        #region Cancel Order
        public async Task<OrderCancelledResponse> CancleOrder(CancelOrder order)
        {
            try
            {
                var result = await _idRepository.CancelOrderAsync(order);
                if (result > 0)
                {
                    var evt = new NotificationEvent
                    {
                        Module = "OrderService",
                        EventType = "OrderCancelled",
                        CustomerEmail = order.customerEmail,
                        Payload = new
                        {
                            orderId = order.orderId,
                            customerId = order.customerId,
                            customerEmail = order.customerEmail,
                            reason = order.reason
                        }
                    };
                    await _eventPublisher.PublishAsync(evt);
                }
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

        #region Ship Order
        public async Task<OrderShippedResponse> ShipOrder(ShipOrderRequest order)
        {
            try
            {
                var number = new Random().Next(100000, 999999);
                order.trackingNumber = $"TRK-{number}";
                order.estimatedDeliveryDate = DateTime.Now.AddDays(7);
                var result = await _idRepository.OrderShippedResponseAsync(order);
                if (result > 0)
                {
                    var evt = new NotificationEvent
                    {
                        Module = "OrderService",
                        EventType = "OrderShipped",
                        CustomerEmail = order.customerEmail,
                        Payload = new
                        {
                            orderId = order.orderId,
                            customerId = order.customerId,
                            customerEmail = order.customerEmail,
                            trackingNumber = order.trackingNumber,
                            estimatedDeliveryDate = order.estimatedDeliveryDate
                        }
                    };
                    await _eventPublisher.PublishAsync(evt);
                }
                return new OrderShippedResponse
                {
                    ResponseCode = result > 0 ? "00" : "01",
                    ResponseMessage = result > 0 ? "Order shipped successfully." : "Failed to ship order.",
                    Details = null!
                };
            }
            catch
            {
                _logger.LogError($"Error shipping order with ID, {order.orderId}");
                return new OrderShippedResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "An error occurred while shipping the order. Please try again later.",
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
                var evt = new NotificationEvent
                {
                    Module = "OrderService",
                    EventType = "OrderCreated",
                    CustomerEmail = order.customerEmail,
                    Payload = new
                    {
                        orderId = order.orderId,
                        customerId = order.customerId,
                        customerEmail = order.customerEmail,
                        item = order.item,
                        amount = order.amount
                    }
                };
                await _eventPublisher.PublishAsync(evt);
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