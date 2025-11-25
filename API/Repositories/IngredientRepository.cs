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

        public async Task<Ingredient> CreateAsync(Ingredient ingredient)
        {
            await _dbContext.Ingredients.AddAsync(ingredient);
            await _dbContext.SaveChangesAsync();
            return ingredient;
        }

        public async Task<ICollection<Ingredient>?> GetAllIngredientsAsync()
        {
            return await _dbContext.Ingredients
                .AsNoTracking()
                .Where(ingredient => !ingredient.isDeleted)
                .ToListAsync();
        }

        public async Task<Ingredient?> GetIngredientByIdAsync(long id)
        {
            return await _dbContext.Ingredients
                .AsNoTracking()      
                .FirstOrDefaultAsync(ingredient => ingredient.Id == id && !ingredient.isDeleted);
        }

        public async Task<Ingredient?> GetIngredientByNameAsync(string name)
        {
            return await _dbContext.Ingredients.AsNoTracking()
                                    .FirstOrDefaultAsync(ingredient => ingredient.Name == name && !ingredient.isDeleted);
        }

        public async Task<ICollection<Ingredient>> GetByIdsAsync(ICollection<long> ids)
        {
            return await _dbContext.Ingredients
                .Where(i => ids.Contains(i.Id) && !i.isDeleted)
                .ToListAsync();
        }

        public async Task<Ingredient> DeleteAsync(long id)
        {
            var ingredient = await _dbContext.Ingredients.FirstOrDefaultAsync(i => i.Id == id && !i.isDeleted);
            if (ingredient == null)
            {
                throw new Exception("Ingredient not found.");
            }
            ingredient.isDeleted = true;
            await _dbContext.SaveChangesAsync();
            return ingredient;
        }
    }
}
