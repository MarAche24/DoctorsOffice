using System.ComponentModel.DataAnnotations;

namespace Arztpraxis.Data.Models
{
    public class Medication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(512)]

        public string Name { get; set; }  = null!;
    }
}
