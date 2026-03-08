using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderServiceApi.Api.Data.Dtos.Requests
{
    public class ShipOrderRequest : BaseRequest
    {
        [JsonIgnore]
        public string trackingNumber { get; set; } = String.Empty;
        [JsonIgnore]
        public DateTime estimatedDeliveryDate { get; set; }
    }
}
