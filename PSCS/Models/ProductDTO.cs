using System.ComponentModel.DataAnnotations;

namespace PSCS.API.Models
{
    public class ProductDTO
    {

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public int SupplierId { get; set; }

        public byte[]? Image {  get; set; }
    }
}
