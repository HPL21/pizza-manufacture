namespace API.Models
{
    public class OrderDetails
    {
        public long PizzaId { get; set; }
        public virtual Pizza Pizza { get; set; } = null!;

        public long OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;
    }
}
