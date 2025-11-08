using API.Interfaces.IRepostories;
using API.Interfaces.IServices;
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
        public async Task<ICollection<Pizza>> GetAllPizzasAsync()
        {
            return await _pizzaRepository.GetAllPizzasAsync();
        }
    }
}
