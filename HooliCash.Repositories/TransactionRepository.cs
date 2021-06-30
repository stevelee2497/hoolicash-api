using HooliCash.Core.Models;
using HooliCash.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HooliCash.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DbContext context) : base(context)
        {
        }

        public override IEnumerable<Transaction> All()
        {
            return Table.Include(x => x.Category).Include(x => x.Wallet).Where(x => x.IsActive).AsEnumerable();
        }
    }
}
