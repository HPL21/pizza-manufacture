namespace API.Exceptions.Pizza
{
    public class PizzaNotFoundException : Exception
    {
        public PizzaNotFoundException()
        {
        }
        public PizzaNotFoundException(string message)
            : base(message)
        {
        }
    }
}
