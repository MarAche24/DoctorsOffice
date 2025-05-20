using Arztpraxis.Data.Models;
using Arztpraxis.InputModels;
using Arztpraxis.ViewModels;
using Arztpraxis.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Arztpraxis.Views
{
    /// <summary>
    /// Interaction logic for TeatmentEdit.xaml
    /// </summary>
    public partial class TeatmentEditView : UserControl
    {
        //public Treatment Treatment { get; private set; }

        public new TreatmentEditViewModel DataContext { get => (TreatmentEditViewModel)base.DataContext; }

        public TeatmentEditView(Treatment? treatment = null)
        {
            //this.Treatment = treatment ?? new Treatment();

            base.DataContext = new TreatmentEditViewModel(treatment);
            InitializeComponent();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext.Save();
            
        }

        private void Add_Medication_Button_Click(object sender, RoutedEventArgs e)
        {
            AddMedicationWindow addMedicationWindow = new AddMedicationWindow();
            addMedicationWindow.Closed += this.AddDialogClosed;
            
            addMedicationWindow.ShowDialog();
        }

        private void AddDialogClosed(object? sender, EventArgs e)
        {
            if (sender is AddMedicationWindow addMedicationWindow)
            {
                if (addMedicationWindow.Add == false)
                {
                    return;
                }

                this.DataContext.AddMedication(addMedicationWindow.Input.MedicationId, addMedicationWindow.Input.Amount);
            }
        }

        private void Remove_Medication_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext.SelectedMedication != null)
            {
                this.DataContext.TreatmentInput.Medication.Remove(this.DataContext.SelectedMedication);
            }
        }
    }
}
