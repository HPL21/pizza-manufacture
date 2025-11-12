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
                OrderItems = order.OrderDetails.Select(od => od.Pizza.toDTO()).ToList()
            };
        }
    }
}
