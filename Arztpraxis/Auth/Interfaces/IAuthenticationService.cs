using Arztpraxis.Auth.Models;
using Arztpraxis.InputModels;

namespace Arztpraxis.Auth.Interfaces
{
    public interface IAuthenticationService
    {
        public AuthenticatedUser? Login(InputUser inputUser);
    }
}
