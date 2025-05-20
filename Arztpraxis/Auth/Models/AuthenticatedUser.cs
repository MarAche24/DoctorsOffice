using Arztpraxis.Data.Models;

namespace Arztpraxis.Auth.Models
{
    public class AuthenticatedUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public int EmployeeId { get; set; }
    }
}
