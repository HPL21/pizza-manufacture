using API.DTOs.PizzaIngredient;

namespace API.DTOs.Pizza
{
    public class PizzaOrderDTO
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public double Weight { get; set; }
        public double Calories { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; } = 1;
        public List<PizzaIngredientDTO> Ingredients { get; set; } = new List<PizzaIngredientDTO>();
    }
}
