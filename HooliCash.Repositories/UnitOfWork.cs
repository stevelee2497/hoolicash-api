using HooliCash.Core.DbContexts;
using HooliCash.IRepositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace HooliCash.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HooliCashContext Context;
        private bool _disposed;
        private IUserRepository _users;
        private IWalletRepository _wallets;
        private ICategoryRepository _categories;
        private ITransactionRepository _transactions;

        public UnitOfWork(HooliCashContext context)
        {
            Context = context;

        }

        public IUserRepository Users => _users ?? (_users = new UserRepository(Context));

        public IWalletRepository Wallets => _wallets ?? (_wallets = new WalletRepository(Context));

        public ICategoryRepository Categories => _categories ?? (_categories = new CategoryRepository(Context));

        public ITransactionRepository Transactions => _transactions ?? (_transactions = new TransactionRepository(Context));

        public IDbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction();
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
                    Context.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
