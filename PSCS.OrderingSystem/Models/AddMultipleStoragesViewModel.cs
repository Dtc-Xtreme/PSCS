using System.ComponentModel.DataAnnotations;

namespace PSCS.OrderingSystem.Models
{
    public class AddMultipleStoragesViewModel
    {
        [Required]
        [MaxLength(1)]
        public string AisleLetter { get; set; }  // [A]0101B1

        [Required]
        [Range(1,99)]
        public int AisleStart { get; set; }     // A[01]01B1

        [Required]
        [Range(1, 99)]
        public int AisleEnd { get; set; }

        [Required]
        [Range(1, 99)]
        public int StackStart { get; set; }     // A01[01]B1
        [Required]
        [Range(1, 99)]
        public int StackEnd { get; set; }

        [Required]
        public char LevelStart { get; set; }    // A0101[B]1
        [Required]
        public char LevelEnd { get; set; }

        [Required]
        [Range(1, 9)]
        public int SectionStart { get; set; }   // A0101B[1]
        [Required]
        [Range(1, 9)]
        public int SectionEnd { get; set; }

        public bool Mloc {  get; set; }
        public bool Mix {  get; set; }
        public bool Blocked { get; set; }
    }
}
