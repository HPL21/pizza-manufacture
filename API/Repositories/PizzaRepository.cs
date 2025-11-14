using API.Data;
using API.Exceptions.Pizza;
using API.Interfaces.IRepostories;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public PizzaRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ICollection<Pizza>?> GetAllPizzasAsync()
        {
            return await _dbContext.Pizzas.AsNoTracking()
                .Where(pizza => !pizza.isDeleted)
                .Include(pizza => pizza.PizzaIngredients)
                .ThenInclude(pi => pi.Ingredient)
                .ToListAsync();
        }

        public async Task<Pizza?> GetPizzaByIdAsync(long id)
        {
            return await _dbContext.Pizzas.AsNoTracking()
                .Where(pizza => !pizza.isDeleted)
                .Include(pizza => pizza.PizzaIngredients)
                .ThenInclude(pi => pi.Ingredient)
                .FirstOrDefaultAsync(pizza => pizza.Id == id);
        }
        public async Task<Pizza> CreateAsync(Pizza pizza)
        {
            await _dbContext.Pizzas.AddAsync(pizza);
            await _dbContext.SaveChangesAsync();
            return pizza;
        }

        public async Task<Pizza> DeleteAsync(long id)
        {
            var pizza = await _dbContext.Pizzas.FirstOrDefaultAsync(p => p.Id == id && !p.isDeleted);
            if (pizza == null)
            {
                throw new PizzaNotFoundException("Pizza not found.");
            }
            pizza.isDeleted = true;
            await _dbContext.SaveChangesAsync();
            return pizza;
        }
    }
}
