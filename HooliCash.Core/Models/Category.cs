using HooliCash.Shared;
using System.Collections.Generic;

namespace HooliCash.Core.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public TransactionType TransactionType { get; set; }

        public string IconUrl { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
