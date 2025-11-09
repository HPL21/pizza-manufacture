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
            var pizzas = await _pizzaService.GetAllPizzasAsync();
            return Ok(pizzas);
        }
        [Authorize]
        [HttpGet("Auth")]
        public async Task<IActionResult> GetAllPizzasAsyncAuthorized()
        {
            var pizzas = await _pizzaService.GetAllPizzasAsync();
            return Ok(pizzas);
        }
    }
}
