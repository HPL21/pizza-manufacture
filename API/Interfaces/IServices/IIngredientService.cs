using API.DTOs.Ingredient;
using API.Models;

namespace API.Interfaces.IServices
{
    public interface IIngredientService
    {
        Task<ICollection<IngredientDTO>> GetAllIngredientsAsync();
        Task<IngredientDTO> GetIngredientByIdAsync(long id);
        Task<IngredientDTO> GetIngredientByNameAsync(string name);
        Task<Ingredient> CreateAsync(CreateIngredientRequestDTO createIngredientRequestDTO);
    }
}
