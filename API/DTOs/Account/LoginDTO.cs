using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Account
{
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
