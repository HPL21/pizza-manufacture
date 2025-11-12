using API.DTOs.Ingredient;

namespace API.Interfaces.IServices
{
    public interface IIngredientService
    {
        Task<ICollection<IngredientDTO>> GetAllIngredientsAsync();
        Task<IngredientDTO> GetIngredientByIdAsync(int id);
        Task<IngredientDTO> GetIngredientByNameAsync(string name);
    }
}
