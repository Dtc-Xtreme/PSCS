using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSCS.Domain
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public Supplier Supplier { get; set; }

        public int SupplierId { get; set; }

        public byte[] Image { get; set; }
    }
}
