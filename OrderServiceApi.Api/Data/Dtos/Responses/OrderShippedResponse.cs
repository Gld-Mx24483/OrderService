namespace OrderServiceApi.Api.Data.Dtos.Responses
{
    public class OrderShippedResponse
    {
        public string ResponseCode { get; set; } = String.Empty;
        public string ResponseMessage { get; set; } = String.Empty;
        public OrderShippedDetails Details { get; set; } = null!;
    }

    public class OrderShippedDetails
    {
        public string TrackingId { get; set; } = String.Empty;
    }
}