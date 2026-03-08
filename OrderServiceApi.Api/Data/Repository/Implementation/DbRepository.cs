using Dapper;
using Microsoft.AspNetCore.Mvc.Filters;
using Oracle.ManagedDataAccess.Client;
using OrderServiceApi.Api.Data.Dtos.Requests;
using OrderServiceApi.Api.Data.Dtos.Responses;
using OrderServiceApi.Api.Data.Repository.Interface;

namespace OrderServiceApi.Api.Data.Repository.Implementation
{
    public class DbRepository : IdRepository
    {
        public string dbConnection = Environment.GetEnvironmentVariable("DatabaseConnection")!;
        private readonly ILogger<DbRepository> _logger;
        public DbRepository(ILogger<DbRepository> logger)
        {
            _logger = logger;
        }

        public async Task<int> OrderShippedResponseAsync(ShipOrderRequest order)
        {
            try
            {
                using(var db = new OracleConnection(dbConnection))
                {
                    await db.OpenAsync();
                    var query = @"UPDATE ORDERS
                             SET Status = 'Shipped',
                                 TrackingNumber = :TrackingNumber,
                                 EstimatedDeliveryDate = :EstimatedDeliveryDate
                             WHERE OrderId = :OrderId
                             AND CustomerId = :CustomerId
                             AND CustomerEmail = :CustomerEmail";
                    var result = await db.ExecuteAsync(query, new
                    {
                        OrderId = order.orderId,
                        TrackingNumber = order.trackingNumber,
                        EstimatedDeliveryDate = order.estimatedDeliveryDate,
                        CustomerId = order.customerId,
                        CustomerEmail = order.customerEmail
                    });
                    return result;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error updating order with ID, {order.orderId} ==> {ex.Message}");
                return 0;
            }
        }

        public async Task<int> CancelOrderAsync(CancelOrder order)
        {
            try
            {
                using (var db = new OracleConnection(dbConnection))
                {
                    await db.OpenAsync();
                    var query = @"UPDATE ORDERS
                             SET Status = 'Cancelled',
                                 Reason = :Reason
                             WHERE OrderId = :OrderId
                             AND CustomerId = :CustomerId
                             AND CustomerEmail = :CustomerEmail";
                    var result = await db.ExecuteAsync(query, new
                    {
                        OrderId = order.orderId,
                        Reason = order.reason,
                        CustomerId = order.customerId,
                        CustomerEmail = order.customerEmail
                    });
                    return result;
                }
                
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error cancelling order with ID, {order.orderId} ==> {ex.Message}");
                return 0;
            }
        }

        public async Task<OrderCreationResponse>CreateOrderAsync(CreateOrderRequest order)
        {
            try
            {
                using(var db = new OracleConnection(dbConnection))
                {
                    await db.OpenAsync();
                    var query = @"INSERT INTO ORDERS
                             (OrderId, CustomerId, CustomerEmail, OrderItem, Amount)
                             VALUES
                             (:OrderId, :CustomerId, :CustomerEmail, :OrderItem, :Amount)";

                    var result = await db.ExecuteAsync(query, new
                    {
                        OrderId = order.orderId,
                        CustomerId = order.customerId,
                        CustomerEmail = order.customerEmail,
                        OrderItem = order.item,
                        Amount = order.amount
                    });

                    return new OrderCreationResponse
                    {
                        ResponseCode = result > 0 ? "00" : "01",
                        ResponseMessage = result > 0 ? "Order created successfully." : "Failed to create order.",
                        Details = new OrderCreationDetails
                        {
                            OrderId = order.orderId
                        }
                    };
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error creating order with ID, {order.orderId} ==> {ex.Message}");
                return new OrderCreationResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "An error occurred while creating the order. Please try again later.",
                    Details = null!
                };
            }

        }
    }
}