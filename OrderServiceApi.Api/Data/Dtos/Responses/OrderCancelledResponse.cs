namespace OrderServiceApi.Api.Data.Dtos.Responses
{
    public class OrderCancelledResponse
    {
        public string ResponseCode { get; set; } = String.Empty;
        public string ResponseMessage { get; set; } = String.Empty;
        public object Details { get; set; } = null!;
    }
}