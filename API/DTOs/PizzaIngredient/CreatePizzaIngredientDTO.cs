using System.ComponentModel.DataAnnotations;

namespace API.DTOs.PizzaIngredient
{
    public class CreatePizzaIngredientDTO
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "IngredientId must greater than 0")]
        public long IngredientId { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Amount of ingredients must greater than 0")]
        public double IngredientAmount { get; set; }
    }
}
