using API.DTOs.PizzaIngredient;

namespace API.DTOs.Pizza
{
    public class PizzaDTO
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public double Weight { get; set; }
        public double Calories { get; set; }
        public double Price { get; set; }

        public List<PizzaIngredientDTO> Ingredients { get; set; } = new List<PizzaIngredientDTO>();
    }
}
