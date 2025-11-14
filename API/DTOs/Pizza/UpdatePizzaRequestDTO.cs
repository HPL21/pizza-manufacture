using API.DTOs.PizzaIngredient;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Pizza
{
    public class UpdatePizzaRequestDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Weight value is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Weight must be greater than 0.")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Calories value is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Calories must be greater than 0.")]
        public double Calories { get; set; }

        [Required(ErrorMessage = "Price value is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Ingredients are required.")]
        [MinLength(1, ErrorMessage = "Pizza must have at least one ingredient")]
        public virtual ICollection<CreatePizzaIngredientDTO> PizzaIngredients { get; set; } = new List<CreatePizzaIngredientDTO>();
    }
}
