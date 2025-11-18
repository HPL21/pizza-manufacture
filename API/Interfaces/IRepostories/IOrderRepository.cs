using API.Models;

namespace API.Interfaces.IRepostories
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderByIdAsync(int id);
        Task<Order?> GetOrderByIdAndUserIdAsync(int id, string userId);
        Task<ICollection<Order>?> GetAllOrdersAsync();
        Task<ICollection<Order>?> GetAllOrdersByUserIdAsync(string userId);
        Task<Order?> CreateAsync(Order order);
    }
}
