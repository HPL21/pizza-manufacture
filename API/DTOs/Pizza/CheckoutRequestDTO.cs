using System.ComponentModel.DataAnnotations;
using API.DTOs.OrderItem;

namespace API.DTOs.Pizza
{
    public class CheckoutRequestDTO
    {
        [Required(ErrorMessage = "Pizzas are required.")]
        [MinLength(1, ErrorMessage = "At least one pizza ID must be provided.")]
        public ICollection<CreateOrderDetailDTO> Pizzas { get; set; } = new List<CreateOrderDetailDTO>();
    }
}
