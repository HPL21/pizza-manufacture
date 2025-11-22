using API.DTOs.Order;
using API.DTOs.Pizza;
using API.Exceptions.Ingredient;
using API.Exceptions.Order;
using API.Exceptions.Pizza;
using API.Interfaces.IRepostories;
using API.Interfaces.IServices;
using API.Mappers;
using API.Models;


namespace API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPizzaService _pizzaService; 
        public OrderService(IOrderRepository orderRepository, IPizzaService pizzaService)
        {
            _orderRepository = orderRepository;
            _pizzaService = pizzaService;
        }

        public async Task<Order> ChangeStatusAsync(ChangeOrderStatusRequestDTO changeStatusRequestDTO, long id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                throw new OrderNotFoundException($"Order with ID {id} was not found.");
            }
            order.Status = changeStatusRequestDTO.Status;
            if (changeStatusRequestDTO.Status == OrderStatus.COMPLETED)
            {
                order.CompletedAt = DateTime.UtcNow;
            }
            return await _orderRepository.ChangeStatusAsync(order);
        }

        public async Task<Order> CreateAsync(CreateOrderRequestDTO createOrderRequestDTO, string UserId)
        {
            var pizzas = await _pizzaService.GetByIdsAsync(createOrderRequestDTO.OrderItems.Select(p => p.PizzaId).ToList());
            var order = await _orderRepository.CreateAsync(createOrderRequestDTO.toModelFromCreateDTO(UserId, pizzas));

            return order;
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

        public async Task<OrderDTO> GetOrderByIdAndUserIdAsync(long id, string userId)
        {
            var order = await _orderRepository.GetOrderByIdAndUserIdAsync(id, userId);
            if (order == null)
            {
                throw new OrderNotFoundException($"Order with ID {id} was not found.");
            }
            return order.toDTO();
        }

        public async Task<OrderDTO> GetOrderByIdAsync(long id)
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
