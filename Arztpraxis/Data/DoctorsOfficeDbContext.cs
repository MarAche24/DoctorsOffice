using Arztpraxis.Data.Models;
using Arztpraxis.Data.Models.Views;
using Microsoft.EntityFrameworkCore;

namespace Arztpraxis.Data
{
    public class DoctorsOfficeDbContext : DbContext
    {
        public virtual DbSet<Medication> Medications { get; set; }

        public virtual DbSet<Person> People { get; set; }
        
        public virtual DbSet<Treatment> Treatments { get; set; }

        public virtual DbSet<User> Users { get; set; }


        public virtual DbSet<Medication_To_Treatment> Medication_To_Treatments { get; set; }

        public virtual DbSet<TreatmentInfo> TreatmentInfos { get; set; }



        public DoctorsOfficeDbContext(DbContextOptions options)
            :base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer(this.ConnectionString);

        //    //optionsBuilder.UseSqlServer();
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medication_To_Treatment>()
                .HasKey(e => new{ e.MedicationID, e.TreatmentID });

            modelBuilder.Entity<Treatment>()
                .HasOne(t => t.Patient)
                .WithMany()

                // This violates the FK integrity of the db. Should be changed. Just a temporary fix for the POC.
                .OnDelete(DeleteBehavior.NoAction);

            // TODO: order view by date
            modelBuilder.Entity<TreatmentInfo>()
                .ToView("View_TreatmentInfo");


            base.OnModelCreating(modelBuilder);
        }
    }
}
