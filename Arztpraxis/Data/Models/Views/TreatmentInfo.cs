using System.ComponentModel.DataAnnotations;

namespace Arztpraxis.Data.Models.Views
{
    public class TreatmentInfo
    {
        [Key]
        public int Id { get; set; }

        public string PatientFirstName { get; set; }

        public string PatientLastName { get; set; }

        public DateOnly StartDate { get; set; }

        public string Memo { get; set; }
    }
}
