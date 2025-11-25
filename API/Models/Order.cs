using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public enum PaymentMethod
    {
        CASH,
        CARD
    }

    public enum OrderStatus
    {
        PLACED,
        IN_PROGRESS,
        IN_DELIVERY,
        COMPLETED,
        CANCELLED
    }

    [Table("Orders")]
    public class Order
    {
        [Key]
        public long Id { get; set; }

        public required String UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public required DateTime  PlacedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public double TotalPrice { get; set; }
        public double TotalWeight { get; set; }
        public double TotalCalories { get; set; }

        public required string RecipientName { get; set; }
        public string? RecipientAddress { get; set; }
        public required string RecipientPhone { get; set; }
        public string? RecipientEmail { get; set; }

        public required PaymentMethod PaymentMethod { get; set; }
        public required OrderStatus Status { get; set; } = OrderStatus.PLACED;

        public bool isDeleted { get; set; } = false;

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
