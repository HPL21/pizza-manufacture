using API.Models;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Order
{
    public class ChangeOrderStatusRequestDTO
    {
        [Required(ErrorMessage = "Order status is required.")]
        public required OrderStatus Status { get; set; }
    }
}
