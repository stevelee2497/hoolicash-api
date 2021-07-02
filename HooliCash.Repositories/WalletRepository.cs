using HooliCash.Core.Models;
using HooliCash.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HooliCash.Repositories
{
    public class WalletRepository : Repository<Wallet>, IWalletRepository
    {
        public WalletRepository(DbContext context) : base(context)
        {
        }

        public override IEnumerable<Wallet> All()
        {
            return Table.Include(x => x.Transactions).Where(x => x.IsActive).AsEnumerable();
        }
    }
}
