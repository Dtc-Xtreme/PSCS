using System.ComponentModel.DataAnnotations;

namespace PSCS.OrderingSystem.Models
{
    public class StorageRequest
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool Mloc { get; set; }

        public bool Mix { get; set; }

        public bool Blocked { get; set; }
    }
}
