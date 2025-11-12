using System.Security.Claims;
using API.Exceptions.Order;
using API.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrdersAsync()
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var orders = await _orderService.GetAllOrdersAsync();
                    return Ok(orders);
                }
                else
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var orders = await _orderService.GetAllOrdersByUserIdAsync(userId!);
                    return Ok(orders);
                }
            }
            catch (OrderNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderByIdAsync(int id)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var order = await _orderService.GetOrderByIdAsync(id);
                    return Ok(order);
                }
                else 
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var order = await _orderService.GetOrderByIdAndUserIdAsync(id, userId!);
                    return Ok(order);
                }         
            }
            catch (OrderNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
