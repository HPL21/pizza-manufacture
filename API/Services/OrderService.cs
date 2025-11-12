using API.DTOs.Order;
using API.Exceptions.Order;
using API.Interfaces.IRepostories;
using API.Interfaces.IServices;
using API.Mappers;


namespace API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<ICollection<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            if (orders == null || !orders.Any()) 
            { 
                throw new OrderNotFoundException("No orders found.");
            }
            return orders.Select(o => o.toDTO()).ToList();
        }

        public async Task<ICollection<OrderDTO>> GetAllOrdersByUserIdAsync(string userId)
        {
            var orders = await _orderRepository.GetAllOrdersByUserIdAsync(userId);
            if (orders == null || !orders.Any())
            {
                throw new OrderNotFoundException("No orders found.");
            }
            return orders.Select(o => o.toDTO()).ToList();
        }

        public async Task<OrderDTO> GetOrderByIdAndUserIdAsync(int id, string userId)
        {
            var order = await _orderRepository.GetOrderByIdAndUserIdAsync(id, userId);
            if (order == null)
            {
                throw new OrderNotFoundException($"Order with ID {id} was not found.");
            }
            return order.toDTO();
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                throw new OrderNotFoundException($"Order with ID {id} was not found.");
            }
            return order.toDTO();
        }
    }
}
