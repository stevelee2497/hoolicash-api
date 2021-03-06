using System;

namespace HooliCash.DTOs.Transaction
{
    public class CreateTransactionDto
    {
        public string Title { get; set; }

        public string Detail { get; set; }

        public decimal Amount { get; set; }

        public string TransactionType { get; set; }

        public DateTimeOffset TransactionDate { get; set; }

        public Guid WalletId { get; set; }

        public Guid CategoryId { get; set; }
    }
}
