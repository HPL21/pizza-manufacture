namespace API.DTOs.Pizza
{
    public class CheckoutResponseDTO
    {
        public double TotalCalories { get; set; }
        public double TotalWeight { get; set; }
        public double TotalPrice { get; set; }
        public ICollection<PizzaDTO> pizzas { get; set; } = new List<PizzaDTO>();
    }
}
