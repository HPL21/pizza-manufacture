using API.Models;

namespace API.Interfaces.IServices
{
    public interface IPizzaService
    {
        Task<ICollection<Pizza>> GetAllPizzasAsync();
    }
}
