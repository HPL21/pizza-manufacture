namespace API.Exceptions.Pizza
{
    public class PizzaWasNotFoundException : Exception
    {
        public PizzaWasNotFoundException()
        {
        }
        public PizzaWasNotFoundException(string message)
            : base(message)
        {
        }
    }
}
