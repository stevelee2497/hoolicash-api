using System;

namespace HooliCash.IHelpers
{
    public interface IPasswordHelper
    {
        string GenerateSalt(DateTimeOffset dateTimeOffset);
        string HashPassword(string password, DateTimeOffset passwordLastUpdatedTime);
    }
}
