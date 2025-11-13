using API.DTOs.Pizza;
using API.Models;

namespace API.Interfaces.IServices
{
    public interface IPizzaService
    {
        Task<ICollection<PizzaDTO>> GetAllPizzasAsync();
        Task<PizzaDTO> GetPizzaByIdAsync(int id);
        Task<Pizza> CreateAsync(CreatePizzaRequestDTO createPizzaRequestDTO);
    }
}
