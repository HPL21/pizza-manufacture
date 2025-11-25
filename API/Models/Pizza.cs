using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Pizzas")]
    public class Pizza
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; }
        public double Weight { get; set; }
        public double Calories { get; set; }
        public double Price { get; set; }

        public bool isDeleted { get; set; } = false;

        public virtual ICollection<PizzaIngredient> PizzaIngredients { get; set; } = new List<PizzaIngredient>();
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
