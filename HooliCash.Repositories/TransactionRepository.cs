using HooliCash.Core.Models;
using HooliCash.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace HooliCash.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DbContext context) : base(context)
        {
        }
    }
}
