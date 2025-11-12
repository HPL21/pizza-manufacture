using API.Models;

namespace API.Interfaces.IRepostories
{
    public interface IIngredientRepository
    {
        Task<ICollection<Ingredient>?> GetAllIngredientsAsync();
        Task<Ingredient?> GetIngredientByIdAsync(int id);
        Task<Ingredient?> GetIngredientByNameAsync(string name);
    }
}
