using HooliCash.Core.Models;
using System.Security.Principal;

namespace HooliCash.IHelpers
{
    public interface ITokenHelper
    {
        string GenerateEncodedToken(User user);
    }
}
