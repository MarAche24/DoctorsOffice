using System.ComponentModel.DataAnnotations;

namespace Arztpraxis.Data.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(512)]

        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(512)]
        public string LastName { get; set; } = null!;

        [Required]
        public DateOnly DateOfBirth { get; set; }

        public bool IsStaff { get; set; } = false;
    }
}
