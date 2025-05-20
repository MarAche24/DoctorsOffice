using Arztpraxis.InputModels;

namespace Arztpraxis.Auth.Interfaces
{
    public interface IPasswordHasher
    {
        public byte[] HashPassword(Guid id, InputUser user);
    }
}
