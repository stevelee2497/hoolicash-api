using System.Collections.Generic;

namespace HooliCash.Core.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string AvatarUrl { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
