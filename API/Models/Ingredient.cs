using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Ingredients")]
    public class Ingredient
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; } = null!;

        public double Price { get; set; }
        public double Weight { get; set; }
        public double Calories { get; set; }

        public bool isDeleted { get; set; } = false;
        public virtual ICollection<PizzaIngredient> PizzaIngredients { get; set; } = new List<PizzaIngredient>();

    }
}
