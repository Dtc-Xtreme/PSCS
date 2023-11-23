using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSCS.Domain
{
    public class OrderLine
    {
        [Key]
        public int Id { get; set; }

        public Product? Product { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int FullPallet {  get; set; }

        public bool Done { get; set; } = false;
    }
}
