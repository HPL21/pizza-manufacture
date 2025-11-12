namespace API.Exceptions.Ingredient
{
    public class IngredientNotFoundException : Exception
    {
        public IngredientNotFoundException()
        {
        }
        public IngredientNotFoundException(string message)
            : base(message)
        {
        }
    }
}
