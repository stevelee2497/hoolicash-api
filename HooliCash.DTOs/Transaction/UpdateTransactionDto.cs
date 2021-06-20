using System;

namespace HooliCash.DTOs.Transaction
{
    public class UpdateTransactionDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Detail { get; set; }

        public decimal Amount { get; set; }

        public string TransactionType { get; set; }

        public Guid WalletId { get; set; }

        public Guid CategoryId { get; set; }
    }
}
