using System.ComponentModel.DataAnnotations;

namespace TestCompurent.Models.Dtos.Client
{
    public class ChangePasswordDto
    {
        [Required]
        [MaxLength(100)]
        public required string NewPassword { get; set; }
    }
}
