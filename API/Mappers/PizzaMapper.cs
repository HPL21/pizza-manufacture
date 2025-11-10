using API.DTOs.Pizza;
using API.Models;

namespace API.Mappers
{
    public static class PizzaMapper
    {
        public static PizzaDTO toDTO(this Pizza pizza)
        {
            return new PizzaDTO
            {
                Id = pizza.Id,
                Name = pizza.Name,
                Weight = pizza.Weight,
                Calories = pizza.Calories,
                Price = pizza.Price,
                Ingredients = pizza.PizzaIngredients.Select(pi => new PizzaIngredientDTO
                {
                    IngredientId = pi.IngredientId,
                    IngredientName = pi.Ingredient.Name,
                    IngredientAmount = pi.IngredientAmount,
                    Price = pi.Ingredient.Price,
                    Weight = pi.Ingredient.Weight,
                    Calories = pi.Ingredient.Calories
                }).ToList()
            };
        }
    }
}
