using Arztpraxis.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public new LoginViewModel DataContext { get => (LoginViewModel)base.DataContext; }

        private readonly static string InvalidInputText = "Benutzername oder Passwort war nicht korrekt.";

        public LoginView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.InvalidTextBlock.Text = string.Empty;

            this.DataContext.Input.ClearPassword = PasswordInput.Password;
            this.DataContext.Login();

            if (App.Current.CurrentUser == null)
            {
                // login failed
                this.InvalidTextBlock.Text = InvalidInputText;
                this.PasswordInput.Password = string.Empty;
            }
        }
    }
}
