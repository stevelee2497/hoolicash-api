using HooliCash.Core.Models;
using HooliCash.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace HooliCash.Repositories
{
    public class WalletRepository : Repository<Wallet>, IWalletRepository
    {
        public WalletRepository(DbContext context) : base(context)
        {
        }
    }
}
