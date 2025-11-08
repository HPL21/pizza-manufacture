using API.Models;

namespace API.Interfaces.IRepostories
{
    public interface IPizzaRepository
    {
        Task<ICollection<Pizza>> GetAllPizzasAsync();
    }
}
