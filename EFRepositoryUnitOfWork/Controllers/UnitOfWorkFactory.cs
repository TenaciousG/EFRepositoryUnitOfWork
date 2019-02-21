using System;
using Effort;
using Effort.DataLoaders;
using EFRepositoryUnitOfWork.Implementations;
using EFRepositoryUnitOfWork.Interfaces;
using EFRepositoryUnitOfWork.Models;

namespace EFRepositoryUnitOfWork.Controllers
{
    public class UnitOfWorkFactory
    {
        public UnitOfWorkFactory()
        {
            this.Context = new MyBankDataModel();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkFactory"/> class.
        /// For unit testing
        /// </summary>
        /// <param name="context">The context.</param>
        public UnitOfWorkFactory(MyBankDataModel context)
        {
            this.Context = context;
        }

        public MyBankDataModel Context { get; set; }

        public IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(Context);
        }
    }
}