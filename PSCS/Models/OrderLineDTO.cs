using System.ComponentModel.DataAnnotations;

namespace PSCS.API.Models
{
    public class OrderLineDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public bool FullPallet { get; set; } = false;
    }
}
