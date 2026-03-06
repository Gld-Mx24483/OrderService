using System.ComponentModel.DataAnnotations;

namespace OrderServiceApi.Api.Data.Dtos.Requests
{
    public class CancelOrder
    {
        [Required]
        public string orderId { get; set; } = String.Empty;
        [EmailAddress]
        public string customerEmail { get; set; } = String.Empty;
        [Required]
        public string customerId { get; set; } = String.Empty;
        [Required]
        public string reason { get; set; } = String.Empty;
    }
}