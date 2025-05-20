using Arztpraxis.Views;
using System.Windows;
using System.Windows.Controls;

namespace Arztpraxis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.ChangeView(new LoginView());

            // TODO: Content can be reassigned to other usercontrol -> navigation
        }

        public void ChangeView(UserControl view)
        {
            this.Page.Content = view;
        }
    }
}