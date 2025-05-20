using Arztpraxis.Data.Models;
using Arztpraxis.Views;
using System.Windows;

namespace Arztpraxis.Windows
{
    /// <summary>
    /// Interaction logic for AddTreatmentWindow.xaml
    /// </summary>
    public partial class EditTreatmentWindow : Window
    {
        public EditTreatmentWindow(Treatment? treatment = null)
        {
            InitializeComponent();
            this.ContentControl.Content = new TeatmentEditView(treatment);
        }
    }
}
