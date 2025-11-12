using API.Data;
using API.Interfaces.IRepostories;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public OrderRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<Order>?> GetAllOrdersAsync()
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Pizza)
                .ThenInclude(p => p.PizzaIngredients)
                .ThenInclude(pi => pi.Ingredient)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAndUserIdAsync(int id, string userId)
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .Include (o => o.User)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Pizza)
                .ThenInclude(p => p.PizzaIngredients)
                .ThenInclude(pi => pi.Ingredient)
                .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Pizza)
                .ThenInclude(p => p.PizzaIngredients)
                .ThenInclude(pi => pi.Ingredient)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<ICollection<Order>?> GetAllOrdersByUserIdAsync(string userId)
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .Where(o => o.UserId == userId)
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Pizza)
                .ThenInclude(p => p.PizzaIngredients)
                .ThenInclude(pi => pi.Ingredient)
                .ToListAsync();
        }
    }
}
