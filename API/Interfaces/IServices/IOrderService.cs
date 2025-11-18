using API.DTOs.Order;

namespace API.Interfaces.IServices
{
    public interface IOrderService
    {
        Task<OrderDTO> GetOrderByIdAsync(int id);
        Task<OrderDTO> GetOrderByIdAndUserIdAsync(int id, string userId);
        Task<ICollection<OrderDTO>> GetAllOrdersAsync();
        Task<ICollection<OrderDTO>> GetAllOrdersByUserIdAsync(string userId);
        Task<OrderDTO> CreateAsync(CreateOrderRequestDTO createOrderRequestDTO, string UserId);
    }
}
