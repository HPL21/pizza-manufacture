using API.Data;
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
                                          .Include(pizza => pizza.PizzaIngredients)
                                          .ThenInclude(pi => pi.Ingredient)
                                          .ToListAsync();
        }

        public async Task<Pizza?> GetPizzaByIdAsync(int id)
        {
            return await _dbContext.Pizzas.AsNoTracking()
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
    }
}
