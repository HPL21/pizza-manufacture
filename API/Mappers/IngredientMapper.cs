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
        public static Ingredient toModelFromCreateDTO(this CreateIngredientRequestDTO createIngredientRequestDTO)
        {
            return new Ingredient
            {
                Name = createIngredientRequestDTO.Name,
                Price = createIngredientRequestDTO.Price,
                Weight = createIngredientRequestDTO.Weight,
                Calories = createIngredientRequestDTO.Calories
            };
        }
    }
}
