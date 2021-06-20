using HooliCash.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HooliCash.IRepositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Add(T entity);

        IEnumerable<T> AddRange(IEnumerable<T> entities);

        IEnumerable<T> All();

        IEnumerable<T> Where(Expression<Func<T, bool>> predicate);

        T Find(Guid id);

        T First();

        T First(Expression<Func<T, bool>> predicate);

        T FirstOrDefault();

        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        bool Any();

        bool Any(Expression<Func<T, bool>> predicate);

        int Count(Expression<Func<T, bool>> predicate);

        int Count();

        T Remove(T entity);

        IEnumerable<T> RemoveRange(IEnumerable<T> entity);

        T Update(T entity);

        IEnumerable<T> UpdateRange(IEnumerable<T> entities);
    }
}
