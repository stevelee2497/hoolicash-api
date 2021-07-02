using HooliCash.Core.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace HooliCash.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IWalletRepository Wallets { get; }
        ICategoryRepository Categories { get; }
        ITransactionRepository Transactions { get; }
        IDbContextTransaction BeginTransaction();
        int Complete();
    }
}
