using HooliCash.Shared;
using System;

namespace HooliCash.Core.Models
{
    public class Transaction : BaseEntity
    {
        public string Title { get; set; }

        public string Detail { get; set; }

        public decimal Amount { get; set; }

        public TransactionType TransactionType { get; set; }

        public DateTimeOffset TransactionDate { get; set; }

        public Guid WalletId { get; set; }

        public Guid CategoryId { get; set; }

        public virtual Wallet Wallet { get; set; }

        public virtual Category Category { get; set; }
    }
}
