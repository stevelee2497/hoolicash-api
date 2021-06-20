using HooliCash.Core.Models;
using System;

namespace HooliCash.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : BaseEntity;

        int Complete();
    }
}
