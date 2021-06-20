using System;

namespace HooliCash.Shared
{
    public static class Jwt
    {
        public static readonly string Secret = "If you are wondering what is it about, then just ignore it :)))))))))";
        public static readonly string DefaultScheme = "JwtBearer";
        public static readonly string Issuer = "HooliCash ecosystem";
        public static readonly string Audience = "Everyone";
        public static readonly TimeSpan TokenLifetime = TimeSpan.FromDays(10);
    }

    public static class JwtClaimType
    {
        public static readonly string UserId = "userId";
        public static readonly string DisplayName = "displayName";
        public static readonly string Email = "email";
    }

    public static class Error
    {
        public static readonly string BadRequest = "Bad Request";
        public static readonly string NotFound = "Not Found";
        public static readonly string InternalServerError = "Internal Server Error";
        public static readonly string InvalidInput = "Invalid Input";
        public static readonly string EmailExist = "Email is already exists.";
        public static readonly string EmailNotExist = "Email is not exists.";
        public static readonly string WrongPassword = "Wrong password";
    }
}
