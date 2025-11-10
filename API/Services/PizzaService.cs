using API.DTOs.Pizza;
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
            return pizzas.Select(p => p.toDTO()).ToList();
        }
    }
}
