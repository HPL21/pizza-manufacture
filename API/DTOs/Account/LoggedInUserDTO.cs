namespace API.DTOs.Account
{
    public class LoggedInUserDTO
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public required string Token { get; set; }

    }
}
