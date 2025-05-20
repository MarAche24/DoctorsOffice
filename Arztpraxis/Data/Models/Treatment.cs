using System.ComponentModel.DataAnnotations;

namespace Arztpraxis.Data.Models
{
    public class Treatment
    {
        [Key]
        public int Id { get; set; }

        public Person Patient { get; set; } = null!;

        public int PatientId { get; set; }

        public DateOnly PrescriptionDate { get; set; }

        [StringLength(4000)]
        public string Memo { get; set; } = string.Empty;

        [Required]
        public User CreatedBy { get; set; } = null!;

        public Guid CreatedById { get; set; }
    }
}
