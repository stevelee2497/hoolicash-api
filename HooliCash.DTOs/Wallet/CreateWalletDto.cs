using System;

namespace HooliCash.DTOs.Wallet
{
    public class CreateWalletDto
    {
        public string Name { get; set; }

        public string IconUrl { get; set; }

        public Guid UserId { get; set; }
    }
}
