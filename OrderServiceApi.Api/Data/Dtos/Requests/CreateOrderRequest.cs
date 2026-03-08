using System.ComponentModel.DataAnnotations;

namespace OrderServiceApi.Api.Data.Dtos.Requests
{
    public class CreateOrderRequest : BaseRequest
    {
        [Required]
        public string item { get; set; } = String.Empty;
        [Required]
        public decimal amount { get; set; }
    }
}
