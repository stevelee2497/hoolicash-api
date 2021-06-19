using HooliCash.Shared;

namespace HooliCash.Core.Models
{
    public class Transaction : BaseEntity
    {
        public string Title { get; set; }

        public string Detail { get; set; }

        public decimal Amount { get; set; }

        public TransactionType TransactionType { get; set; }

        public virtual User Owner { get; set; }

        public virtual Wallet Wallet { get; set; }

        public virtual Category Category { get; set; }
    }
}
