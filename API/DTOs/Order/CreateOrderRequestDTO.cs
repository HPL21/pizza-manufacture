using System.ComponentModel.DataAnnotations;
using API.DTOs.OrderItem;
using API.DTOs.Pizza;
using API.Models;

namespace API.DTOs.Order
{
    public class CreateOrderRequestDTO
    {
        [Required(ErrorMessage = "Recipent name is required.")]
        [MaxLength(200, ErrorMessage = "Recipent name cannot be longer than 200 characters.")]
        public required string RecipientName { get; set; }

        [MaxLength(200, ErrorMessage = "Recipent address cannot be longer than 200 characters.")]
        public string? RecipientAddress { get; set; }

        [Required(ErrorMessage = "Recipent phone is required.")]
        [RegularExpression(@"^(\+48)?\s?\d{3}[-\s]?\d{3}[-\s]?\d{3}$", ErrorMessage = "Provide a valid phone number.")]
        public required string RecipientPhone { get; set; }

        [EmailAddress(ErrorMessage = "Provide a valid email address.")]
        public string? RecipientEmail { get; set; }

        [Required(ErrorMessage = "Payment method is required.")]
        public required PaymentMethod PaymentMethod { get; set; }

        [Required(ErrorMessage = "Order items are required.")]
        [MinLength(1, ErrorMessage = "Order must have at least one item")]
        public virtual ICollection<CreateOrderDetailDTO> OrderItems { get; set; } = new List<CreateOrderDetailDTO>();
    }
}
