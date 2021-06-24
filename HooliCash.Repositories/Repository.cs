using HooliCash.Core.Models;
using HooliCash.IRepositories;
using HooliCash.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HooliCash.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext Context;
        private readonly DbSet<T> Table;

        public Repository(DbContext context)
        {
            Context = context;
            Table = Context.Set<T>();
        }

        public virtual T Add(T entity)
        {
            return Table.Add(OnCreate(entity)).Entity;
        }

        public virtual IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            return entities.Select(x => Table.Add(OnCreate(x)).Entity);
        }

        public virtual IEnumerable<T> All()
        {
            return Table.Where(x => x.IsActive).AsEnumerable();
        }

        public virtual bool Any(Expression<Func<T, bool>> predicate)
        {
            return Table.Where(x => x.IsActive).Any(predicate);
        }

        public virtual bool Any()
        {
            return Table.Any(x => x.IsActive);
        }

        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            return Table.Where(x => x.IsActive).Count(predicate);
        }

        public virtual int Count()
        {
            return Table.Count(x => x.IsActive);
        }

        public virtual T Find(Guid id)
        {
            var res = Table.Find(id);
            return res?.IsActive == true ? res : throw new HooliCashException($"Cound not find record {typeof(T).Name} id {id}");
        }

        public virtual T First()
        {
            return Table.Where(x => x.IsActive).First();
        }

        public virtual T First(Expression<Func<T, bool>> predicate)
        {
            return Table.Where(x => x.IsActive).First(predicate);
        }

        public virtual T FirstOrDefault()
        {
            return Table.Where(x => x.IsActive).FirstOrDefault();
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return Table.Where(x => x.IsActive).FirstOrDefault(predicate);
        }

        public virtual T Remove(T entity)
        {
            return Table.Update(OnRemove(entity)).Entity;
        }

        public virtual IEnumerable<T> RemoveRange(IEnumerable<T> entities)
        {
            return entities.Select(x => Table.Update(OnRemove(x)).Entity);
        }

        public virtual T Update(T entity)
        {
            return Table.Update(OnUpdate(entity)).Entity;
        }

        public virtual IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            return entities.Select(x => Table.Update(OnUpdate(x)).Entity);
        }

        public virtual IEnumerable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return Table.Where(x => x.IsActive).Where(predicate);
        }

        private T OnCreate(T entity)
        {
            entity.CreatedOn = DateTime.Now;
            entity.UpdatedOn = DateTime.Now;
            entity.IsActive = true;
            return entity;
        }

        private T OnUpdate(T entity)
        {
            entity.UpdatedOn = DateTime.Now;
            return entity;
        }

        private T OnRemove(T entity)
        {
            entity.IsActive = false;
            entity.UpdatedOn = DateTime.Now;
            return entity;
        }
    }
}
