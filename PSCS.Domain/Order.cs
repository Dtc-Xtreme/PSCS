using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSCS.Domain
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

        [Required]
        public Zone Zone { get; set; }
        public int ZoneId { get; set; }
    }
}
