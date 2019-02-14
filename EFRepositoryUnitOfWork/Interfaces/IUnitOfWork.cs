using System;

namespace EFRepositoryUnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        void Save();
        void Dispose();
    }
}
