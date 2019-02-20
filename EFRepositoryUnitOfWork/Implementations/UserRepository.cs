using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EFRepositoryUnitOfWork.Interfaces;
using EFRepositoryUnitOfWork.Models;

namespace EFRepositoryUnitOfWork.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MyBankDataModel context)
            : base(context)
        { }

        public IEnumerable<User> GetUsersWithFirstName(string firstName)
        {
            return this.MyBankDataModel.MyUsers.Where(x => x.FirstName.Equals(firstName));
        }

        public MyBankDataModel MyBankDataModel => this.Context as MyBankDataModel;
    }
}
