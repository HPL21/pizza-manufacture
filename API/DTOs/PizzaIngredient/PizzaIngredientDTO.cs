namespace API.DTOs.PizzaIngredient
{
    public class PizzaIngredientDTO
    {
        public long IngredientId { get; set; }
        public required string IngredientName { get; set; }
        public double IngredientAmount { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
        public double Calories { get; set; }
    }
}
