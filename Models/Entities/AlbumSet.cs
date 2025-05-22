using System.ComponentModel.DataAnnotations;

namespace TestCompurent.Models.Entities
{
    public class AlbumSet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required float Price { get; set; }

    }
}
