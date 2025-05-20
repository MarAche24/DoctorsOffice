using Arztpraxis.Data;
using Arztpraxis.Data.Models;
using Arztpraxis.InputModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Arztpraxis.Windows
{
    /// <summary>
    /// Interaction logic for AddMedicationWindow.xaml
    /// </summary>
    public partial class AddMedicationWindow : Window
    {
        public bool Add { get; set; }

        public MedicationToTreatmentInput Input { get; set; } = new MedicationToTreatmentInput();

        public List<Medication> Medications { get; set; }

        private IDbContextFactory<DoctorsOfficeDbContext> dbContextFactory;

        public AddMedicationWindow()
        {
            dbContextFactory = App.host.Services.GetService<IDbContextFactory<DoctorsOfficeDbContext>>()
                ?? throw new Exception("No Database connection. Cannot add medication");

            this.LoadMedications();

            InitializeComponent();
        }

        [MemberNotNull(nameof(this.Medications))]
        private void LoadMedications()
        {
            using DoctorsOfficeDbContext dbContext = dbContextFactory.CreateDbContext();
            this.Medications = dbContext.Medications.ToList();
        }

        private void IsNumber(object sender, TextCompositionEventArgs e)
        {
            // set false if parsing is successful
            e.Handled = !this.InputIsNumber(e.Text);
        }

        private bool InputIsNumber(string input)
        {
            return !input.IsNullOrEmpty()
                && int.TryParse(input, out int amount)
                && amount > 0;

        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {

            if (this.InputIsValid())
            {
                this.Input.Amount = int.Parse(this.AmountInput.Text);

                this.Add = true;
                this.Close();
                return;
            }

            MessageBox.Show("Eingabe unvollständig.", "Fehler", MessageBoxButton.OK);
        }

        private bool InputIsValid()
        {
            return this.InputIsNumber(this.AmountInput.Text)
                && this.Input.MedicationId != 0
                && this.Input.MedicationId > 0;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
