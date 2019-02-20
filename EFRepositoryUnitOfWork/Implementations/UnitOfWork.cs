using System;
using EFRepositoryUnitOfWork.Interfaces;
using EFRepositoryUnitOfWork.Models;

namespace EFRepositoryUnitOfWork.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyBankDataModel context;
        public IUserRepository Users { get; }

        public UnitOfWork(MyBankDataModel context)
        {
            this.context = context;
            this.Users = new UserRepository(context);
        }
        public void Save()
        {
            this.context.SaveChanges();
        }
        
        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
