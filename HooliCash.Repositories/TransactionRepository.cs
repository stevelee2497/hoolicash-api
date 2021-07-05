using HooliCash.Core.Models;
using HooliCash.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        public override IEnumerable<Transaction> Where(Expression<Func<Transaction, bool>> predicate)
        {
            return Table.Include(x => x.Category).Include(x => x.Wallet).Where(x => x.IsActive).Where(predicate);
        }
    }
}
