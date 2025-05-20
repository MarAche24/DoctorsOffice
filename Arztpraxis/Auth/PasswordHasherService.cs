using Arztpraxis.Auth.Interfaces;
using Arztpraxis.InputModels;
using System.Security.Cryptography;
using System.Text;

namespace Arztpraxis.Auth
{
    /// <summary>
    /// Hashing service used to hash passwords
    /// </summary>
    public class PasswordHasherService : IPasswordHasher
    {
        // debug databases are not compatible with production databases
        private static int hashingIterations =
#if DEBUG
            10;
#else
            10000;
#endif

        /// <summary>
        /// Hashes the password of the user based on the SHA256 algorithm.
        /// </summary>
        /// <param name="id">Id of the saved user / Id for the new user.</param>
        /// <param name="user">User. Contains the password to hash.</param>
        /// <returns>hashed password as a byte array.</returns>
        public byte[] HashPassword(Guid id, InputUser user)
        {
            string idAsString = id.ToString();
            string firstIdHalf = idAsString[..(idAsString.Length / 2)];

            string lastUsernameHalf = user.Username[(user.Username.Length / 2)..];

            string salt = firstIdHalf + lastUsernameHalf;
            return this.HashPassword(user.ClearPassword, salt);
        }

        private byte[] HashPassword(string clearPassword, string salt)
        {
            byte[] inputbytes = Encoding.UTF8.GetBytes(clearPassword + salt);

            for (int i = 0; i < hashingIterations; i++)
            {
                inputbytes = SHA256.HashData(inputbytes);
            }

            return inputbytes;
        }
    }
}
