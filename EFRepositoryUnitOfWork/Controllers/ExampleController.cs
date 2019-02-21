using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using Effort;
using Effort.DataLoaders;
using EFRepositoryUnitOfWork.Implementations;
using EFRepositoryUnitOfWork.Interfaces;
using EFRepositoryUnitOfWork.Models;

namespace EFRepositoryUnitOfWork.Controllers
{
    [RoutePrefix("example")]
    public class ExampleController : ApiController
    {
        private UnitOfWorkFactory unitOfWorkFactory;

        //IUnitOfWork unitOfWork;
        public ExampleController(UnitOfWorkFactory unitOfWorkFactory)
        {
            this.UnitOfWorkFactory = unitOfWorkFactory;
            //this.unitOfWork = new UnitOfWork<MyBankDataModel>();
        }

        public ExampleController()
        {
            
        }

        //[Route("AccountForUser")]
        //public HttpResponseMessage GetAccountsForUser(string userFirstName)
        //{
        //    IRepository<AccountJunction> accountJunctionRepo = this.unitOfWork.GetRepository<AccountJunction>();
        //    var accountJunctions = accountJunctionRepo.Get(x => x.User.FirstName == userFirstName);
        //    var result = accountJunctions.Select(aj => new
        //    {
        //        AccountNumber = aj.Account.AccountNumber,
        //        AccountType = aj.Account.Type,
        //        Balance = aj.Account.Balance
        //    }).ToList();

        //    return Request.CreateResponse(HttpStatusCode.OK, result);
        //}

        //[Route("Accounts")]
        //public HttpResponseMessage GetAccounts()
        //{
        //    using (var unitOfWork = new UnitOfWork(new MyBankDataModel()))
        //    {
        //        IRepository<Account> accountRepo = unitOfWork.Accounts;
        //        var accounts = accountRepo.Get(x => x.Type.Equals("Checking"));
        //        var result = accounts.Select(a => new
        //        {
        //            AccountType = a.Type, Balance = a.Balance
        //        }).ToList();

        //        return Request.CreateResponse(HttpStatusCode.OK, result);
        //    }
        //}

        [Route("Users")]
        public HttpResponseMessage GetUsersWithNameJohn()
        {
            using (var unitOfWork = UnitOfWorkFactory.CreateUnitOfWork())
            {
                var users = unitOfWork.Users;
                var johnUser = users.GetUsersWithFirstName("John").FirstOrDefault();
                var result = new UserDto
                {
                    FirstName = johnUser.FirstName,
                    LastName = johnUser.LastName,
                };

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        public UnitOfWorkFactory UnitOfWorkFactory
        {
            get => this.unitOfWorkFactory ?? (this.unitOfWorkFactory = new UnitOfWorkFactory());
            set => this.unitOfWorkFactory = value;
        }
    }

    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
