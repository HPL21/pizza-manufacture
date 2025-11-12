namespace API.DTOs.Ingredient
{
    public class IngredientDTO
    {
        public long id { get; set; }
        public required string name { get; set; }
        public double price { get; set; }
        public double weight { get; set; }
        public double calories { get; set; }
    }
}
