using HooliCash.Core.Models;

namespace HooliCash.IHelpers
{
    public interface ITokenHelper
    {
        string GenerateEncodedToken(User user);
    }
}
