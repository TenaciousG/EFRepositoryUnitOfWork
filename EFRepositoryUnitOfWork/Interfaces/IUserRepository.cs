using System.Collections.Generic;
using EFRepositoryUnitOfWork.Models;

namespace EFRepositoryUnitOfWork.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetUsersWithFirstName(string firstName);
    }
}
