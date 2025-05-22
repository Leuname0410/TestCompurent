using System.ComponentModel.DataAnnotations;

namespace TestCompurent.Models.Dtos.Client
{
    public class LoginDto
    {
        [Required]
        [MaxLength(10)]
        public string Id { get; set; } = string.Empty;

        [Required]
        public required string Password { get; set; }
    }
}
