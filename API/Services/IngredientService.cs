using API.DTOs.Ingredient;
using API.Exceptions.Ingredient;
using API.Interfaces.IRepostories;
using API.Interfaces.IServices;
using API.Mappers;

namespace API.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public async Task<ICollection<IngredientDTO>> GetAllIngredientsAsync()
        {
            var ingredients = await _ingredientRepository.GetAllIngredientsAsync();
            return ingredients?.Select(i => i.toDTO()).ToList() ?? new List<IngredientDTO>();
        }

        public async Task<IngredientDTO> GetIngredientByIdAsync(int id)
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
    }
}
