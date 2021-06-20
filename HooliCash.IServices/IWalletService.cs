using HooliCash.DTOs.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HooliCash.IServices
{
    public interface IWalletService
    {
        WalletDto CreateWallet(CreateWalletDto createWalletDto);
        IEnumerable<WalletDto> GetWallets(Guid userId);
        WalletDto GetWallet(Guid walletId);
        WalletDto UpdateWallet(UpdateWalletDto updateWalletDto);
        bool DeleteWallet(Guid walletId);
    }
}
