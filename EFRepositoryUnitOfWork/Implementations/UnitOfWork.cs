using System;
using System.Data.Entity;
using EFRepositoryUnitOfWork.Interfaces;
using EFRepositoryUnitOfWork.Models;

namespace EFRepositoryUnitOfWork.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyBankDataModel context;
        public IUserRepository Users { get; }

        public UnitOfWork()
        {
            var myBankDataModel = new MyBankDataModel();
            this.context = myBankDataModel;
            this.Users = new UserRepository(context);
        }

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
