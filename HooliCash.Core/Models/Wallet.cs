using System;
using System.Collections.Generic;

namespace HooliCash.Core.Models
{
    public class Wallet : BaseEntity
    {
        public string Name { get; set; }

        public string IconUrl { get; set; }

        public Guid UserId { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public virtual User User { get; set; }
    }
}
