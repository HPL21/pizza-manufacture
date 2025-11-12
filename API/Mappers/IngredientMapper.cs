using API.DTOs.Ingredient;
using API.Models;

namespace API.Mappers
{
    public static class IngredientMapper
    {
        public static IngredientDTO toDTO (this Ingredient ingredient)
        {
            return new IngredientDTO
            {
                id = ingredient.Id,
                name = ingredient.Name,
                price = ingredient.Price,
                weight = ingredient.Weight,
                calories = ingredient.Calories
            };
        }
    }
}
