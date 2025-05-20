using Arztpraxis.Auth;
using Arztpraxis.Auth.Models;
using Arztpraxis.InputModels;
using Arztpraxis.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Arztpraxis.ViewModels
{
    public class LoginViewModel 
    {
        public InputUser Input { get; set; }

        public AuthenticationService AuthService { get; set; }

        public LoginViewModel()
        {
            this.Input = new InputUser();
            this.AuthService = App.host.Services.GetService<AuthenticationService>()
                ?? throw new Exception("No AuthenticatonService supplied. Cannot start Login process.");
        }

        public void Login()
        {
            if (this.InputIsValid())
            {
                AuthenticatedUser? authUser = this.AuthService.Login(this.Input);
                if (authUser != null)
                {
                    // navigates to the treatment list view and sets the logged in user globally
                    App.Current.CurrentUser = authUser;
                    App.Current.MainWindow.ChangeView(new TreatmentsListView());
                    return;
                }
            }

            this.InputInvalid();
        }

        private void InputInvalid()
        {
            this.Input.ClearPassword = string.Empty;
        }

        private bool InputIsValid()
        {
#if DEBUG
            // all inputs are valid
            return true;
#else
            return this.UsernameIsValid()
                && this.PasswordIsValid();
#endif
        }

        private bool UsernameIsValid()
        {
            return !this.Input.Username.IsNullOrEmpty()
                && this.Input.Username.Length >= 6;
        }

        private bool PasswordIsValid()
        {
            return !this.Input.ClearPassword.IsNullOrEmpty()
                && this.Input.ClearPassword.Length >= 8;
        }
    }
}
