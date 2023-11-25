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

        public Zone Zone { get; set; }

        [Required]
        public int ZoneId { get; set; }
    }
}
