using Arztpraxis.ViewModels;
using Arztpraxis.Windows;
using System.Windows.Controls;

namespace Arztpraxis.Views
{
    /// <summary>
    /// Interaction logic for TreatmentsList.xaml
    /// </summary>
    public partial class TreatmentsListView : UserControl
    {
        public new TreatmentListViewModel DataContext => (TreatmentListViewModel)base.DataContext;

        public TreatmentsListView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            EditTreatmentWindow addDialog = new EditTreatmentWindow();
            addDialog.Closed += this.ReloadTreatments;
            addDialog.ShowDialog();
        }

        private void ReloadTreatments(object? sender, EventArgs e)
        {
            // This may not work. Binding is still on the old List?
            this.DataContext.LoadTreatments();
        }
    }
}
