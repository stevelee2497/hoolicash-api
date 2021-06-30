using System;

namespace HooliCash.DTOs.User
{
    public class LoginResponseDto
    {
        public Guid UserId { get; set; }

        public string AccessToken { get; set; }
    }
}
