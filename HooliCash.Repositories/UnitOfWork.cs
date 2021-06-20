using HooliCash.Core.DbContexts;
using HooliCash.Core.Models;
using HooliCash.IRepositories;
using System;
using System.Collections.Generic;

namespace HooliCash.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HooliCashContext Context;
        private readonly Dictionary<Type, object> Repositories;
        private bool _disposed;

        public UnitOfWork(HooliCashContext context)
        {
            Context = context;
            Repositories = new Dictionary<Type, object>();
        }

        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            var type = typeof(T);
            if (!Repositories.ContainsKey(type))
            {
                Repositories[type] = new Repository<T>(Context);
            }

            return (IRepository<T>)Repositories[type];
        }

        public int Complete()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (Repositories != null)
                    {
                        Repositories.Clear();
                    }

                    Context.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
