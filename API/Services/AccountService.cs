using API.DTOs.Account;
using API.Exceptions.Account;
using API.Interfaces;
using API.Interfaces.IServices;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signInManager;
        public AccountService(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }
        public async Task<LoggedInUserDTO> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDTO.Username);
            if (user == null) throw new AccountNotFoundException("Account with matching credentials was not found");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if (!result.Succeeded)
            {
                throw new AccountNotFoundException("Account with matching credentials was not found");
            }
            return
                new LoggedInUserDTO
                {
                    Username = user.UserName!,
                    Email = user.Email!,
                    Token = _tokenService.CreateToken(user)
                };
        }

        public async Task<LoggedInUserDTO> Register(RegisterDTO registerDTO)
        {
            try
            {

                var appUser = new User
                {
                    UserName = registerDTO.Username,
                    Email = registerDTO.Email,
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDTO.Password);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return
                            new LoggedInUserDTO
                            {
                                Username = appUser.UserName!,
                                Email = appUser.Email!,
                                Token = _tokenService.CreateToken(appUser)
                            };
                    }
                    else
                    {
                        var errors = string.Join("; ", roleResult.Errors.Select(e => e.Description));
                        throw new AccountNotCreatedException(errors);
                    }
                }
                else
                {
                    var errors = string.Join("; ", createdUser.Errors.Select(e => e.Description));
                    throw new AccountNotCreatedException(errors);
                }
            }
            catch (Exception ex)
            {
                throw new AccountNotCreatedException(ex.Message);
            }
        }
    }
}
