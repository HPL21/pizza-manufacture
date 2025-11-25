using API.Models;

namespace API.Interfaces.IRepostories
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderByIdAsync(long id);
        Task<Order?> GetOrderByIdAndUserIdAsync(long id, string userId);
        Task<ICollection<Order>?> GetAllOrdersAsync();
        Task<ICollection<Order>?> GetAllOrdersByUserIdAsync(string userId);
        Task<Order> CreateAsync(Order order);
        Task<Order> ChangeStatusAsync(Order order);
    }
}
