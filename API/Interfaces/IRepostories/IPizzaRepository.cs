using API.Models;

namespace API.Interfaces.IRepostories
{
    public interface IPizzaRepository
    {
        Task<ICollection<Pizza>?> GetAllPizzasAsync();
        Task<Pizza?> GetPizzaByIdAsync(long id);
        Task<Pizza> CreateAsync(Pizza pizza);
        Task<Pizza> DeleteAsync(long id);
    }
}
