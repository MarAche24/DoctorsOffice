using System.ComponentModel.DataAnnotations;

namespace Arztpraxis.Data.Models
{
    public class Medication_To_Treatment
    {
        [Required]
        public Medication Medication { get; set; } = null!;
        
        public int MedicationID { get; set; }
        
        [Required]
        public Treatment Treatment { get; set; } = null!;

        public int TreatmentID { get; set; }

        public int Amount { get; set; }
    }
}
