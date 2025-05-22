using System.ComponentModel.DataAnnotations;

namespace TestCompurent.Models.Entities
{
    public class Client
    {
        [Key]
        [MaxLength(10)]
        [Required]
        public required string Id { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(50)]
        [Required]
        [EmailAddress(ErrorMessage = "The email must have valid format")]
        public required string Mail { get; set; }

        [MaxLength(500)]
        public string? Direction { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(100)]

        public string? Password { get; set; }


    }
}
