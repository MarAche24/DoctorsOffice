using System.ComponentModel.DataAnnotations;

namespace Arztpraxis.Data.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 6)]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(512)]
        public byte[] PasswordHash { get; set; } = null!;

        // TODO: implemented later
        //public DateTime LastLogin {  get; set; }

        [Required]
        public Person Employee { get; set; } = null!;

        public int EmployeeId { get; set; }
    }
}
