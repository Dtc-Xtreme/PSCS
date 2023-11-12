using PSCS.Domain;
using System.ComponentModel.DataAnnotations;

namespace PSCS.API.Models
{
    public class OrderDTO
    {
        [Required]
        public int ZoneId {  get; set; }

        public List<OrderLineDTO> OrderLines { get; set; }
    }
}
