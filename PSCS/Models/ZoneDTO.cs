using System.ComponentModel.DataAnnotations;

namespace PSCS.API.Models
{
    public class ZoneDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
