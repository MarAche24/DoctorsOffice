using Arztpraxis.Data.Models;
using System.Collections.ObjectModel;

namespace Arztpraxis.InputModels
{
    public class TreatmentInput
    {
        public int? Id { get; set; } = null;

        public int PatientId { get; set; }

        public DateTime PrescriptionDate { get; set; } = DateTime.Now;

        public string Memo { get; set; } = string.Empty;

        public ObservableCollection<MedicationInput> Medication { get; set; } = [];
    }
}
