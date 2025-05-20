using Arztpraxis.Auth.Interfaces;
using Arztpraxis.Auth.Models;
using Arztpraxis.Data;
using Arztpraxis.Data.Models;
using Arztpraxis.InputModels;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Arztpraxis.Auth
{
    /// <summary>
    /// Service used to handle the Authentication process.
    /// </summary>
    /// <param name="dbContextFactory"></param>
    public class AuthenticationService(IDbContextFactory<DoctorsOfficeDbContext> dbContextFactory, IPasswordHasher passwordHasher) : IAuthenticationService
    {
        /// <summary>
        /// Login method. Checks if user exists and if the password is valid.
        /// </summary>
        /// <param name="inputUser">Usercredentials. Used for authentification.</param>
        /// <returns>An AuthenticatedUser instance if the user is valid. </returns>
        public AuthenticatedUser? Login(InputUser inputUser)
        {


            User? savedUser;
            using (DoctorsOfficeDbContext dbContext = dbContextFactory.CreateDbContext())
            {

#if DEBUG
                // all passwords are valid
                return new AuthenticatedUser()
                {
                    Id = dbContext.Users.First().Id,
                    Username = "Debug - Admin",
                    EmployeeId = -1,
                };
#endif


                //dbContext.Users.FromSqlRaw("SET IDENTITY_INSERT dbo.Users ON;");

                //Guid id = Guid.NewGuid();

                //dbContext.Add(new User()
                //{
                //    Id = id,
                //    Username = "martin",
                //    PasswordHash = passwordHasher.HashPassword(id, inputUser),
                //    EmployeeId = 1
                //});

                //dbContext.SaveChanges();
                //dbContext.Users.FromSqlRaw("SET IDENTITY_INSERT dbo.Users OFF;");

                savedUser = dbContext.Users.SingleOrDefault(u => u.Username == inputUser.Username);
            }

            // username was not correct
            if (savedUser == null) return null;

            byte[] hashedInputPassword = passwordHasher.HashPassword(savedUser.Id, inputUser);
            if (hashedInputPassword.SequenceEqual(savedUser.PasswordHash))
            {
                return new AuthenticatedUser()
                {
                    Id = savedUser.Id,
                    Username = inputUser.Username,
                    EmployeeId = savedUser.EmployeeId, 
                };
            }

            // password was not correct
            return null;
        }
    }
}
