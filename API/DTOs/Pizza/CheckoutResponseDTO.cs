namespace API.DTOs.Pizza
{
    public class CheckoutResponseDTO
    {
        public double TotalCalories { get; set; }
        public double TotalWeight { get; set; }
        public double TotalPrice { get; set; }
        public ICollection<PizzaOrderDTO> pizzas { get; set; } = new List<PizzaOrderDTO>();
    }
}
