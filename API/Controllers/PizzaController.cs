using API.DTOs.Pizza;
using API.Exceptions.Ingredient;
using API.Exceptions.Pizza;
using API.Interfaces.IServices;
using API.Mappers;
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
        public async Task<IActionResult> GetAllPizzas()
        {
            try
            {
                var pizzas = await _pizzaService.GetAllPizzasAsync();
                return Ok(pizzas);
            }
            catch (PizzaNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPizzaById(int id)
        {
            try
            {
                var pizza = await _pizzaService.GetPizzaByIdAsync(id);
                return Ok(pizza);
            }
            catch (PizzaNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreatePizza([FromBody] CreatePizzaRequestDTO createPizzaRequestDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var createdPizza = await _pizzaService.CreateAsync(createPizzaRequestDTO);
                return CreatedAtAction(nameof(GetPizzaById), new { id = createdPizza.Id }, createdPizza.toDTO());
            }
            catch (IngredientNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizza(int id)
        {
            try
            {
                var deletedPizza = await _pizzaService.DeleteAsync(id);
                return NoContent();
            }
            catch (PizzaNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdatePizza([FromBody] UpdatePizzaRequestDTO updatePizzaRequestDTO, long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var updatedPizza = await _pizzaService.UpdateAsync(updatePizzaRequestDTO, id);
                return CreatedAtAction(nameof(GetPizzaById), new { id = updatedPizza.Id }, updatedPizza.toDTO());
            }
            catch (PizzaNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (IngredientNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
