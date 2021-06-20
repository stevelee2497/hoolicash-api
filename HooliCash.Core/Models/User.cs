using System;
using System.Collections.Generic;

namespace HooliCash.Core.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; }

        public string DisplayName { get; set; }

        public string PasswordHash { get; set; }

        public DateTimeOffset PasswordLastUpdatedTime { get; set; }

        public string AvatarUrl { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
