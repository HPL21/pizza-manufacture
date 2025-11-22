using API.DTOs.Ingredient;
using API.DTOs.Pizza;
using API.Exceptions.Ingredient;
using API.Exceptions.Pizza;
using API.Interfaces.IRepostories;
using API.Interfaces.IServices;
using API.Mappers;
using API.Models;

namespace API.Services
{
    public class PizzaService : IPizzaService
    {
        private readonly IPizzaRepository _pizzaRepository;
        private readonly IIngredientService _ingredientService;
        public PizzaService(IPizzaRepository pizzaRepository, IIngredientService ingredientService)
        {
            _pizzaRepository = pizzaRepository;
            _ingredientService = ingredientService;
        }
        public async Task<ICollection<PizzaDTO>> GetAllPizzasAsync()
        {
            var pizzas = await _pizzaRepository.GetAllPizzasAsync();
            if (pizzas == null || !pizzas.Any())
            {
                throw new PizzaNotFoundException("No pizzas found.");
            }
            return pizzas.Select(p => p.toDTO()).ToList();
        }

        public async Task<PizzaDTO> GetPizzaByIdAsync(long id)
        {
            var pizza = await _pizzaRepository.GetPizzaByIdAsync(id);
            if (pizza == null)
            {
                throw new PizzaNotFoundException($"Pizza with ID {id} was not found.");
            }
            return pizza.toDTO();
        }

        public async Task<Pizza> CreateAsync(CreatePizzaRequestDTO createPizzaRequestDTO)
        {
            var ingredients = await _ingredientService.GetByIdsAsync(createPizzaRequestDTO.PizzaIngredients.Select(i => i.IngredientId).ToList());
            return await _pizzaRepository.CreateAsync(createPizzaRequestDTO.toModelFromCreateDTO());
        }

        public async Task<Pizza> DeleteAsync(long id)
        {
            return await _pizzaRepository.DeleteAsync(id);
        }

        public async Task<Pizza> UpdateAsync(UpdatePizzaRequestDTO updatePizzaRequestDTO, long id)
        {
            if (await _pizzaRepository.DeleteAsync(id) == null)
            {
                throw new PizzaNotFoundException($"Pizza with ID {id} was not found.");
            }
            var ingredients = await _ingredientService.GetByIdsAsync(updatePizzaRequestDTO.PizzaIngredients.Select(i => i.IngredientId).ToList());
            return await _pizzaRepository.CreateAsync(updatePizzaRequestDTO.toModelFromUpdateDTO());
        }

        public async Task<ICollection<Pizza>> GetByIdsAsync(ICollection<long> ids)
        {
            var pizzas = await _pizzaRepository.GetByIdsAsync(ids);
            var missingPizzas = ids.Except(pizzas.Select(p => p.Id)).ToList();
            if (missingPizzas.Count > 0)
            {
                throw new PizzaNotFoundException($"Pizza with ids: {string.Join(", ", missingPizzas)} were not found");
            }
            return pizzas;
        }

        public async Task<CheckoutResponseDTO> CheckoutAsync(CheckoutRequestDTO checkoutRequestDTO)
        {
            var pizzas = await GetByIdsAsync(checkoutRequestDTO.Pizzas.Select(p => p.PizzaId).ToList());

            var pizzaOrders = checkoutRequestDTO.Pizzas
                .Select(req =>
                {
                    var pizza = pizzas.First(p => p.Id == req.PizzaId);
                    return pizza.toPizzaOrderDTO((int)req.ItemAmount);
                })
                .ToList();

            return new CheckoutResponseDTO
            {
                TotalPrice = pizzaOrders.Sum(po => po.Price * po.Amount),
                TotalCalories = pizzaOrders.Sum(po => po.Calories * po.Amount),
                TotalWeight = pizzaOrders.Sum(po => po.Weight * po.Amount),
                pizzas = pizzaOrders
            };
        }
    }
}
