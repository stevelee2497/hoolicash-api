using System;

namespace HooliCash.DTOs.User
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }

        public DateTimeOffset PasswordLastUpdatedTime { get; set; }

        public string AvatarUrl { get; set; }

        public int TransactionCount { get; set; }

        public int WalletCount { get; set; }
    }
}
