using System.ComponentModel.DataAnnotations;

namespace API.DTOs.OrderItem
{
    public class CreateOrderDetailDTO
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "PizzaId must greater than 0")]
        public long PizzaId { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Amount of items must greater than 0")]
        public double ItemAmount { get; set; }
    }
}
