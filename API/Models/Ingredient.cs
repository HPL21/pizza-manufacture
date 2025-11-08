using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Ingredient
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; } = null!;

        public double Price { get; set; }
        public double Weight { get; set; }
        public double Calories { get; set; }

        public virtual ICollection<PizzaIngredient> PizzaIngredients { get; set; } = new List<PizzaIngredient>();

    }
}
