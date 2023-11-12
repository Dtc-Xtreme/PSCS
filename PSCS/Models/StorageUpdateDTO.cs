using System.ComponentModel.DataAnnotations;

namespace PSCS.API.Models
{
    public class StorageUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Mloc { get; set; }
        public bool Mix { get; set; } = false;
        public bool Blocked { get; set; } = false;
    }
}
