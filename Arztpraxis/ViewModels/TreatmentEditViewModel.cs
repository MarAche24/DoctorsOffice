using Arztpraxis.Data;
using Arztpraxis.Data.Models;
using Arztpraxis.InputModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Arztpraxis.ViewModels
{
    public class TreatmentEditViewModel
    {
        public Dictionary<int, string> AllPatients { get; set; }

        private IDbContextFactory<DoctorsOfficeDbContext> dbContextFactory;

        //public List<MedicationInput> Medications { get; set; }

        public TreatmentInput TreatmentInput { get; set; }

        public MedicationInput? SelectedMedication {  get; set; }

        public TreatmentEditViewModel(Treatment? treatment = null)
        {
            if (treatment == null)
            {
                this.TreatmentInput = new();
            }
            else
            {
                // TODO: populate inputmodel

                //this.TreatmentInput = new TreatmentInput();
            }

            dbContextFactory = App.host.Services.GetService<IDbContextFactory<DoctorsOfficeDbContext>>()
                ?? throw new Exception("No Database connected. Cannot add or edit treatment.");

            this.LoadAllPatients();
        }

        [MemberNotNull(nameof(this.AllPatients))]
        private void LoadAllPatients()
        {
            using DoctorsOfficeDbContext dbContext = dbContextFactory.CreateDbContext();

            // TODO: Make view
            this.AllPatients = dbContext.People
                .ToList()
                .Select(p => new KeyValuePair<int, string>(p.Id, $"{p.FirstName} {p.LastName} - {p.DateOfBirth}"))
                .ToDictionary();
        }

        public void AddMedication(int id, int amount)
        {
            using DoctorsOfficeDbContext dbContext = dbContextFactory.CreateDbContext();

            // TODO: What if the id does not exist?
            Medication savedMedication = dbContext.Medications.Single(m => m.Id == id);

            this.TreatmentInput.Medication.Add(new MedicationInput
            {
                Id = savedMedication.Id,
                Amount = amount,
                Name = savedMedication.Name,
                //TreatmentId = 
            });
        }

        public bool Save()
        {
            if (!this.InputIsValid())
            {
                return false;
            }

            using DoctorsOfficeDbContext dbContext = this.dbContextFactory.CreateDbContext();



            Treatment savedTreatment = dbContext.Treatments.SingleOrDefault(t => t.Id == this.TreatmentInput.Id) ?? new Treatment();

            savedTreatment.PrescriptionDate = DateOnly.FromDateTime(this.TreatmentInput.PrescriptionDate);
            savedTreatment.PatientId = this.TreatmentInput.PatientId;
            savedTreatment.CreatedById = App.Current.CurrentUser.Id;
            savedTreatment.Memo = this.TreatmentInput.Memo;

            // TODO: does this change if Id is already set?
            dbContext.Add(savedTreatment);
            dbContext.SaveChanges();

            dbContext.Medication_To_Treatments
                .Where(t => t.TreatmentID == savedTreatment.Id)
                .ExecuteDelete();

            IEnumerable<Medication_To_Treatment> newConnections = this.TreatmentInput.Medication.Select(m => new Medication_To_Treatment()
            {
                MedicationID = m.Id,
                TreatmentID = savedTreatment.Id, // is this set?
                Amount = m.Amount,
            });

            dbContext.AddRange(newConnections);
            dbContext.SaveChanges();

            return true;
        }

        private bool InputIsValid()
        {
            // TODO: Add validation
            return true;
        }

        //[MemberNotNull(nameof(this.Medications))]
        //private void LoadMedications()
        //{
        //    using DoctorsOfficeDbContext dbContext = dbContextFactory.CreateDbContext();

        //    // TODO: Make view
        //    this.Medications = dbContext.Medication_To_Treatments
        //        .Where(t => t.TreatmentID == this.Treatment.Id)
        //        .Include(t => t.Medication)
        //        .Select(t =>
        //            new MedicationInput()
        //            {
        //                Id = t.MedicationID,
        //                Name = t.Medication.Name,
        //                Amount = t.Amount,
        //                TreatmentId = t.TreatmentID,
        //            })
        //        .ToList();
        //}
    }
}
