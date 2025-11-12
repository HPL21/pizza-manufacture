using API.Exceptions.Ingredient;
using API.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;
        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIngredientsAsync()
        {
            try
            {
                var ingredients = await _ingredientService.GetAllIngredientsAsync();
                return Ok(ingredients);
            }
            catch (IngredientNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredientByIdAsync(int id)
        {
            try
            {
                var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
                return Ok(ingredient);
            }
            catch (IngredientNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetIngredientByNameAsync(string name)
        {
            try
            {
                var ingredient = await _ingredientService.GetIngredientByNameAsync(name);
                return Ok(ingredient);
            }
            catch (IngredientNotFoundException ex)
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
