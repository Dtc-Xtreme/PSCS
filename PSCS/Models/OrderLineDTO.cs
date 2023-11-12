using System.ComponentModel.DataAnnotations;

namespace PSCS.API.Models
{
    public class OrderLineDTO
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
