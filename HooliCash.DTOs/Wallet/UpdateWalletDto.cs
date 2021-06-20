using System;

namespace HooliCash.DTOs.Wallet
{
    public class UpdateWalletDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string IconUrl { get; set; }

    }
}
