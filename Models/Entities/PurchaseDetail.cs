using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestCompurent.Models.Entities
{
    public class PurchaseDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Client")]
        [MaxLength(10)]
        public required string Client_Id { get; set; }

        [Required]
        [ForeignKey("Album")]
        public required int Album_Id { get; set; }

        [Required]
        public required float Total { get; set; }

        [Required]
        public required DateTime PurchaseDate { get; set; }

    }
}
