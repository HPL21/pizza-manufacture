using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("PizzaIngredients")]
    public class PizzaIngredient
    {
        public long PizzaId { get; set; }
        public virtual Pizza Pizza { get; set; } = null!;

        public long IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; } = null!;

        public double IngredientAmount { get; set; }
    }
}
