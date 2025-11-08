using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}
