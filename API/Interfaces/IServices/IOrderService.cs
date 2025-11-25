using API.DTOs.Order;
using API.Models;

namespace API.Interfaces.IServices
{
    public interface IOrderService
    {
        Task<OrderDTO> GetOrderByIdAsync(long id);
        Task<OrderDTO> GetOrderByIdAndUserIdAsync(long id, string userId);
        Task<ICollection<OrderDTO>> GetAllOrdersAsync();
        Task<ICollection<OrderDTO>> GetAllOrdersByUserIdAsync(string userId);
        Task<Order> CreateAsync(CreateOrderRequestDTO createOrderRequestDTO, string UserId);
        Task<Order> ChangeStatusAsync(ChangeOrderStatusRequestDTO changeStatusRequestDTO, long id);
    }
}
