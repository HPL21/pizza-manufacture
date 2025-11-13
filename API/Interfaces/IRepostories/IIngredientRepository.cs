using API.Models;

namespace API.Interfaces.IRepostories
{
    public interface IIngredientRepository
    {
        Task<ICollection<Ingredient>?> GetAllIngredientsAsync();
        Task<Ingredient?> GetIngredientByIdAsync(long id);
        Task<Ingredient?> GetIngredientByNameAsync(string name);
        Task<Ingredient> CreateAsync(Ingredient ingredient);
        Task<ICollection<Ingredient>> GetByIdsAsync(ICollection<long> ids);
    }
}
