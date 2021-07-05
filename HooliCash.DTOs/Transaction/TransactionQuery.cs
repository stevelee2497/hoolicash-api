using System;

namespace HooliCash.DTOs.Transaction
{
    public class TransactionQuery
    {
        public Guid? UserId { get; set; }

        public Guid? WalletId { get; set; }

        public DateTimeOffset? From { get; set; }

        public DateTimeOffset? To { get; set; }
    }
}
