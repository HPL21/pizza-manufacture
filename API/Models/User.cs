using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class User : IdentityUser
    {
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
