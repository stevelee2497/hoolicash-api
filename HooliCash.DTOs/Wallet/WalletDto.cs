using System;

namespace HooliCash.DTOs.Wallet
{
    public class WalletDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string IconUrl { get; set; }

        public int TransactionCount { get; set; }

        public decimal Balance { get; set; }
    }
}
