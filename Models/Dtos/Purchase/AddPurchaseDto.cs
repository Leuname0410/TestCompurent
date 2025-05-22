using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestCompurent.Models.Dtos.Purchase
{
    public class AddPurchaseDto
    {

        [Required]
        public required int Album_Id { get; set; }

        [Required]
        public required DateTime PurchaseDate { get; set; }
    }
}
