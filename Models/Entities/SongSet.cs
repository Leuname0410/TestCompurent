using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestCompurent.Models.Entities
{
    public class SongSet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public required int Album_Id { get; set; }

    }
}
