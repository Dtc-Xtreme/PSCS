using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSCS.Domain
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1000,9999)]
        public int Number { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]

        public string Address { get; set; }

        [Required]
        [MaxLength(30)]
        public string Phone { get; set; }

        public override string ToString()
        {
            return Name + " (" + Number + ")";
        }
    }
}
