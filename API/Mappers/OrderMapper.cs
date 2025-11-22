using API.DTOs.Order;
using API.DTOs.Pizza;
using API.Models;

namespace API.Mappers
{
    public static class OrderMapper
    {
        public static OrderDTO toDTO(this Order order)
        {
            return new OrderDTO
            {
                Id = order.Id,
                Username = order.User.UserName!,
                PlacedAt = order.PlacedAt,
                CompletedAt = order.CompletedAt,
                TotalPrice = order.TotalPrice,
                RecipientName = order.RecipientName,
                RecipientAddress = order.RecipientAddress,
                RecipientPhone = order.RecipientPhone,
                RecipientEmail = order.RecipientEmail,
                PaymentMethod = order.PaymentMethod,
                Status = order.Status,
                OrderItems = order.OrderDetails.Select(od => od.Pizza.toDTO()).ToList()
            };
        }
        public static Order toModelFromCreateDTO(this CreateOrderRequestDTO createOrderRequestDTO, string UserId, ICollection<Pizza> pizzas)
        {
            return new Order
            {
                UserId = UserId,
                PlacedAt = DateTime.UtcNow,
                TotalPrice = pizzas.Sum(p => p.Price),
                TotalCalories = pizzas.Sum(p => p.Calories),
                TotalWeight = pizzas.Sum(p => p.Weight),
                RecipientName = createOrderRequestDTO.RecipientName,
                RecipientAddress = createOrderRequestDTO.RecipientAddress,
                RecipientPhone = createOrderRequestDTO.RecipientPhone,
                RecipientEmail = createOrderRequestDTO.RecipientEmail,
                PaymentMethod = createOrderRequestDTO.PaymentMethod,
                Status = OrderStatus.PLACED,
                OrderDetails = [.. createOrderRequestDTO.OrderItems.Select(oi => new OrderDetail
                {
                    PizzaId = oi.PizzaId,
                    ItemAmount = oi.ItemAmount
                })]
            };
        }
    }
}
