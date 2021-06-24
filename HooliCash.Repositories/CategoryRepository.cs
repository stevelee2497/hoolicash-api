using HooliCash.Core.Models;
using HooliCash.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HooliCash.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext context) : base(context)
        {
        }

        public override IEnumerable<Category> All()
        {
            return Table.Include(x => x.Transactions).Where(x => x.IsActive).AsEnumerable();
        }
    }
}
