using System.ComponentModel.DataAnnotations;

namespace OrderServiceApi.Api.Data.Dtos.Requests
{
    public class CancelOrder : BaseRequest
    {
        [Required]
        public string reason { get; set; } = String.Empty;
    }
}