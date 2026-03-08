using System.ComponentModel.DataAnnotations;

namespace OrderServiceApi.Api.Data.Dtos.Requests
{
    public class BaseRequest
    {
        [Required]
        public string orderId { get; set; } = String.Empty;
        [EmailAddress]
        public string customerEmail { get; set; } = String.Empty;
        [Required]
        public string customerId { get; set; } = String.Empty;
    }
}