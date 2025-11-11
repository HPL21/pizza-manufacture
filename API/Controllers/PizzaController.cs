using API.Exceptions.Pizza;
using API.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaService _pizzaService;
        public PizzaController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPizzasAsync()
        {
            try
            {
                var pizzas = await _pizzaService.GetAllPizzasAsync();
                return Ok(pizzas);
            }
            catch (PizzaWasNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize]
        [HttpGet("Auth")]
        public async Task<IActionResult> GetAllPizzasAsyncAuthorized()
        {
                try
                {
                    var pizzas = await _pizzaService.GetAllPizzasAsync();
                    return Ok(pizzas);
                }
                catch (PizzaWasNotFoundException ex)
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
