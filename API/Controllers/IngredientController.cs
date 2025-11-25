using API.DTOs.Ingredient;
using API.Exceptions.Ingredient;
using API.Interfaces.IServices;
using API.Mappers;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> GetAllIngredients()
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

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetIngredientById(long id)
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
        public async Task<IActionResult> GetIngredientByName(string name)
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateIngredient([FromBody] CreateIngredientRequestDTO createIngredientRequestDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var createdIngredient = await _ingredientService.CreateAsync(createIngredientRequestDTO);
                return CreatedAtAction(nameof(GetIngredientById), new { id = createdIngredient.Id }, createdIngredient.toDTO());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteIngredient(long id)
        {
            try
            {
                var deletedIngredient = await _ingredientService.DeleteAsync(id);
                return NoContent();
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

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateIngredient([FromBody] UpdateIngridientRequestDTO updateIngridientRequestDTO, long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var updatedIngredient = await _ingredientService.UpdateAsync(updateIngridientRequestDTO, id);
                return Ok(updatedIngredient.toDTO());
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
