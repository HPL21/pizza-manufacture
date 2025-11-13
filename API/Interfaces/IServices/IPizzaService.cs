using API.DTOs.Pizza;
using API.Models;

namespace API.Interfaces.IServices
{
    public interface IPizzaService
    {
        Task<ICollection<PizzaDTO>> GetAllPizzasAsync();
        Task<PizzaDTO> GetPizzaByIdAsync(long id);
        Task<Pizza> CreateAsync(CreatePizzaRequestDTO createPizzaRequestDTO);
        Task<Pizza> DeleteAsync(long id);
    }
}
