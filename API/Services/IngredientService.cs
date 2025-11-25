using API.DTOs.Ingredient;
using API.Exceptions.Ingredient;
using API.Exceptions.Pizza;
using API.Interfaces.IRepostories;
using API.Interfaces.IServices;
using API.Mappers;
using API.Models;

namespace API.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public async Task<Ingredient> CreateAsync(CreateIngredientRequestDTO createIngredientRequestDTO)
        {
            return await _ingredientRepository.CreateAsync(createIngredientRequestDTO.toModelFromCreateDTO());
        }

        public async Task<Ingredient> DeleteAsync(long id)
        {
            return await _ingredientRepository.DeleteAsync(id);
        }

        public async Task<ICollection<IngredientDTO>> GetAllIngredientsAsync()
        {
            var ingredients = await _ingredientRepository.GetAllIngredientsAsync();
            if (ingredients == null || !ingredients.Any())
            {
                throw new IngredientNotFoundException("No ingredients found.");
            }
            return ingredients.Select(i => i.toDTO()).ToList();
        }

        public async Task<ICollection<Ingredient>> GetByIdsAsync(ICollection<long> ids)
        {
            var ingredients = await _ingredientRepository.GetByIdsAsync(ids);
            var missingIngredients = ids.Except(ingredients.Select(p => p.Id)).ToList();
            if (missingIngredients.Count > 0)
            {
                throw new PizzaNotFoundException($"Ingredients with ids: {string.Join(", ", missingIngredients)} were not found");
            }
            return ingredients;
        }

        public async Task<IngredientDTO> GetIngredientByIdAsync(long id)
        {
            var ingredient = await _ingredientRepository.GetIngredientByIdAsync(id);
            if (ingredient == null)
            {
                throw new IngredientNotFoundException($"Ingredient with ID {id} was not found.");
            }
            return ingredient.toDTO();
        }

        public async Task<IngredientDTO> GetIngredientByNameAsync(string name)
        {
            name = char.ToUpper(name[0]) + name.Substring(1).ToLower();
            var ingredient = await _ingredientRepository.GetIngredientByNameAsync(name);
            if (ingredient == null)
            {
                throw new IngredientNotFoundException($"Ingredient with name {name} was not found.");
            }
            return ingredient.toDTO();
        }

        public async Task<Ingredient> UpdateAsync(UpdateIngridientRequestDTO updateIngridientRequestDTO, long id)
        {
            if (await _ingredientRepository.DeleteAsync(id) == null)
            {
                throw new IngredientNotFoundException($"Ingredient with ID {id} was not found.");
            }
            var ingredient = await _ingredientRepository.CreateAsync(updateIngridientRequestDTO.toModelFromUpdateDTO());
            return ingredient;
        }
    }
}
