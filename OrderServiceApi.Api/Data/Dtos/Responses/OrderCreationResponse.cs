namespace OrderServiceApi.Api.Data.Dtos.Responses
{
    public class OrderCreationResponse
    {
        public string ResponseCode { get; set; } = String.Empty;
        public string ResponseMessage { get; set; } = String.Empty;
        public OrderCreationDetails Details { get; set; } = null!;
    }

    public class OrderCreationDetails
    {
        public string OrderId { get; set; } = String.Empty;
    }
}