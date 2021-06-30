using HooliCash.DTOs.Category;
using HooliCash.DTOs.Wallet;
using System;

namespace HooliCash.DTOs.Transaction
{
    public class TransactionDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Detail { get; set; }

        public decimal Amount { get; set; }

        public string TransactionType { get; set; }

        public string TransactionDate { get; set; }

        public virtual WalletDto Wallet { get; set; }

        public virtual CategoryDto Category { get; set; }
    }
}
