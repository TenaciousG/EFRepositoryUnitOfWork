using System;
using System.Collections;
using System.Data.Entity;
using EFRepositoryUnitOfWork.Interfaces;

namespace EFRepositoryUnitOfWork.Implementations
{
    public class UnitOfWork<C> : IDisposable, IUnitOfWork where C : DbContext, new()
    {
        private C context = new C();
        private Hashtable repositories = new Hashtable();
        public IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.Contains(typeof(T)))
            {
                this.repositories.Add(typeof(T), new Repository<T>(this.context));
            }
            return (IRepository<T>)this.repositories[typeof(T)];
        }
        public void Save()
        {
            this.context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}