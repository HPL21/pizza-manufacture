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
        public async Task<ICollection<Pizza>> GetAllPizzasAsync()
        {
            return await _dbContext.Pizzas.Include(pizza => pizza.PizzaIngredients)
                                         .ThenInclude(pi => pi.Ingredient)
                                         .ToListAsync();
        }

    }
}
