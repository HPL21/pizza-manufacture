using API.DTOs.Account;
using API.Models;

namespace API.Interfaces.IServices
{
    public interface IAccountService
    {
        Task<LoggedInUserDTO> Register(RegisterDTO registerDTO);
        Task<LoggedInUserDTO> Login(LoginDTO loginDTO);
    }
}
