using API.DTOs.Pizza;
using API.Models;

namespace API.DTOs.Order
{
    public class OrderDTO
    {
        public long Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public DateTime PlacedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public double TotalPrice { get; set; }
        public double TotalWeight { get; set; }
        public double TotalCalories { get; set; }
        public required string RecipientName { get; set; }
        public string? RecipientAddress { get; set; }
        public required string RecipientPhone { get; set; }
        public string? RecipientEmail { get; set; }
        public required PaymentMethod PaymentMethod { get; set; }
        public required OrderStatus Status { get; set; }
        public virtual ICollection<PizzaOrderDTO> OrderItems { get; set; } = new List<PizzaOrderDTO>();
    }
}
