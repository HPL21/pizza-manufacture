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

        public async Task<Order?> GetOrderByIdAndUserIdAsync(long id, string userId)
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .Where(o => !o.isDeleted)
                .Include (o => o.User)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Pizza)
                .ThenInclude(p => p.PizzaIngredients)
                .ThenInclude(pi => pi.Ingredient)
                .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);
        }

        public async Task<Order?> GetOrderByIdAsync(long id)
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
                .Where(o => o.UserId == userId && !o.isDeleted)
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Pizza)
                .ThenInclude(p => p.PizzaIngredients)
                .ThenInclude(pi => pi.Ingredient)
                .ToListAsync();
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order> ChangeStatusAsync(Order order)
        {
            _dbContext.Orders.Attach(order);
            _dbContext.Entry(order).Property(o => o.Status).IsModified = true;
            _dbContext.Entry(order).Property(o => o.CompletedAt).IsModified = true;

            await _dbContext.SaveChangesAsync();
            return order;
        }
    }
}
