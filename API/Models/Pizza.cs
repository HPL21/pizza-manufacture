using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Pizza
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; }
        public double Weight { get; set; }
        public double Calories { get; set; }
        public double Price { get; set; }

        public virtual ICollection<PizzaIngredient> PizzaIngredients { get; set; } = new List<PizzaIngredient>();
    }
}
