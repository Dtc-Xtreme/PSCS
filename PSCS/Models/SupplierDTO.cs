using System.ComponentModel.DataAnnotations;

namespace PSCS.API.Models
{
    public class SupplierDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Range(1000, 9999)]
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
    }
}
