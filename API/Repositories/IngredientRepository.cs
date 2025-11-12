using API.Data;
using API.Interfaces.IRepostories;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public IngredientRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<Ingredient>?> GetAllIngredientsAsync()
        {
            return await _dbContext.Ingredients.AsNoTracking().ToListAsync();
        }

        public async Task<Ingredient?> GetIngredientByIdAsync(int id)
        {
            return await _dbContext.Ingredients.AsNoTracking()
                                    .FirstOrDefaultAsync(ingredient => ingredient.Id == id);
        }

        public async Task<Ingredient?> GetIngredientByNameAsync(string name)
        {
            return await _dbContext.Ingredients.AsNoTracking()
                                    .FirstOrDefaultAsync(ingredient => ingredient.Name == name);
        }
    }
}
