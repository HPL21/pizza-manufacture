using API.DTOs.Ingredient;
using API.DTOs.PizzaIngredient;
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
        public static Ingredient toModelFromUpdateDTO(this UpdateIngridientRequestDTO updateIngredientRequestDTO)
        {
            return new Ingredient
            {
                Name = updateIngredientRequestDTO.Name,
                Price = updateIngredientRequestDTO.Price,
                Weight = updateIngredientRequestDTO.Weight,
                Calories = updateIngredientRequestDTO.Calories
            };
        }

        public static PizzaIngredientDTO toPizzaIngredientDTO(this Ingredient ingredient, double ingredientAmount)
        {
            return new PizzaIngredientDTO
            {
                IngredientId = ingredient.Id,
                IngredientName = ingredient.Name,
                IngredientAmount = ingredientAmount,
                Price = ingredient.Price,
                Weight = ingredient.Weight,
                Calories = ingredient.Calories
            };
        }
    }
}
