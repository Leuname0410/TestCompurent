using System.ComponentModel.DataAnnotations;

namespace TestCompurent.Models.Dtos.Client
{
    public class UpdateClientDto
    {
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailAddress(ErrorMessage = "The email must have valid format")]
        public string Mail { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Direction { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }
    }
}
