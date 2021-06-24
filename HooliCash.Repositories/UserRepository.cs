using HooliCash.Core.Models;
using HooliCash.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace HooliCash.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public override User Add(User entity)
        {
            return base.Add(entity);
        }
    }
}
