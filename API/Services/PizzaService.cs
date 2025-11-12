using API.DTOs.Pizza;
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
        public PizzaService(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }
        public async Task<ICollection<PizzaDTO>> GetAllPizzasAsync()
        {
            var pizzas = await _pizzaRepository.GetAllPizzasAsync();
            if (pizzas == null || !pizzas.Any())
            {
                throw new PizzaWasNotFoundException("No pizzas found.");
            }
            return pizzas.Select(p => p.toDTO()).ToList();
        }

        public async Task<PizzaDTO> GetPizzaByIdAsync(int id)
        {
            var pizza = await _pizzaRepository.GetPizzaByIdAsync(id);
            if (pizza == null)
            {
                throw new PizzaWasNotFoundException($"Pizza with ID {id} was not found.");
            }
            return pizza.toDTO();
        }
    }
}
