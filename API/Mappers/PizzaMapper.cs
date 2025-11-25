using API.DTOs.Pizza;
using API.DTOs.PizzaIngredient;
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

        public static Pizza toModelFromCreateDTO(this CreatePizzaRequestDTO createPizzaRequestDTO,ICollection<PizzaIngredientDTO> pizzaIngredientDTO)
        {
            return new Pizza
            {
                Name = createPizzaRequestDTO.Name,
                Weight = pizzaIngredientDTO.Sum(pi => pi.Weight * pi.IngredientAmount),
                Calories = pizzaIngredientDTO.Sum(pi => pi.Calories * pi.IngredientAmount),
                Price = createPizzaRequestDTO.Price,
                PizzaIngredients = createPizzaRequestDTO.PizzaIngredients.Select(i => new PizzaIngredient
                {
                    IngredientId = i.IngredientId,
                    IngredientAmount = i.IngredientAmount
                }).ToList()
            };
        }

        public static Pizza toModelFromUpdateDTO(this UpdatePizzaRequestDTO updatePizzaRequestDTO)
        {
            return new Pizza
            {
                Name = updatePizzaRequestDTO.Name,
                Weight = updatePizzaRequestDTO.Weight,
                Calories = updatePizzaRequestDTO.Calories,
                Price = updatePizzaRequestDTO.Price,
                PizzaIngredients = updatePizzaRequestDTO.PizzaIngredients.Select(i => new PizzaIngredient
                {
                    IngredientId = i.IngredientId,
                    IngredientAmount = i.IngredientAmount
                }).ToList()
            };
        }

        public static PizzaOrderDTO toPizzaOrderDTO(this Pizza pizza,int amount)
        {
            return new PizzaOrderDTO
            {
                Id = pizza.Id,
                Name = pizza.Name,
                Weight = pizza.Weight,
                Calories = pizza.Calories,
                Price = pizza.Price,
                Amount = amount,
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
