using HooliCash.DTOs.Wallet;
using HooliCash.IServices;
using System;
using System.Collections.Generic;

namespace HooliCash.Services
{
    public class WalletService : IWalletService
    {
        public WalletDto CreateWallet(CreateWalletDto createWalletDto)
        {
            throw new NotImplementedException();
        }

        public bool DeleteWallet(Guid walletId)
        {
            throw new NotImplementedException();
        }

        public WalletDto GetWallet(Guid walletId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WalletDto> GetWallets(Guid userId)
        {
            throw new NotImplementedException();
        }

        public WalletDto UpdateWallet(UpdateWalletDto updateWalletDto)
        {
            throw new NotImplementedException();
        }
    }
}
