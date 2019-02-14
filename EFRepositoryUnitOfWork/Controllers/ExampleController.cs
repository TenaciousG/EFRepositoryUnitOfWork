using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using EFRepositoryUnitOfWork.Implementations;
using EFRepositoryUnitOfWork.Interfaces;
using EFRepositoryUnitOfWork.Models;

namespace EFRepositoryUnitOfWork.Controllers
{
    [RoutePrefix("example")]
    public class ExampleController : ApiController
    {
        IUnitOfWork unitOfWork;
        public ExampleController()
        {
            this.unitOfWork = new UnitOfWork<MyBankDataModel>();
        }

        [Route("AccountForUser")]
        public HttpResponseMessage GetAccountsForUser(string userFirstName)
        {
            IRepository<AccountJunction> accountJunctionRepo = this.unitOfWork.GetRepository<AccountJunction>();
            var accountJunctions = accountJunctionRepo.Get(x => x.User.FirstName == userFirstName);
            var result = accountJunctions.Select(aj => new
            {
                AccountNumber = aj.Account.AccountNumber,
                AccountType = aj.Account.Type,
                Balance = aj.Account.Balance
            }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("Accounts")]
        public HttpResponseMessage GetAccounts()
        {
            IRepository<Account> accountRepo = this.unitOfWork.GetRepository<Account>();
            var accounts = accountRepo.Get(x => x.Type.Equals("Checking"));
            var result = accounts.Select(a => new
            {
                AccountType = a.Type,
                Balance = a.Balance
            }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("Users")]
        public HttpResponseMessage GetUsers()
        {
            IRepository<User> users = this.unitOfWork.GetRepository<User>();
            var johnUser = users.Get(x => x.FirstName.Equals("John")).FirstOrDefault();
            var result = new
            {
                FirstName = johnUser.FirstName,
                LastName = johnUser.LastName,
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        //[Route("User")]
        //public HttpResponseMessage GetUser()
        //{
        //    var users = this.unitOfWork.GetRepository<User>();
        //    var user = users.Get(x => x.FirstName.Equals("John")).FirstOrDefault();
        //    var result = new
        //    {
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        SSN = user.SSN
        //    };
        //    return this.Request.CreateResponse(HttpStatusCode.OK, result);
        //}

        //[Route("User")]
        //[HttpPost]
        //public HttpResponseMessage PostUser(AddUserRequest request)
        //{
        //    var hrm = new HttpResponseMessage();
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(request?.FirstName) || string.IsNullOrWhiteSpace(request?.LastName) || string.IsNullOrWhiteSpace(request?.SSN))
        //        {
        //            this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "FirstName, LastName, and SSN are required.");
        //        }
        //        else
        //        {
        //            var users = this.unitOfWork.GetRepository<User>();
        //            var user = users.Get(x => x.SSN == request.SSN).FirstOrDefault();
        //            if (user == null)
        //            {
        //                //If the user is null then create a new user
        //                user = new User()
        //                {
        //                    FirstName = request.FirstName,
        //                    LastName = request.LastName,
        //                    SSN = request.SSN
        //                };
        //                users.Insert(user);
        //            }
        //            else
        //            {
        //                //If the user already exists then just update
        //                user.FirstName = request.FirstName;
        //                user.LastName = request.LastName;
        //                users.Update(user);
        //            }
        //            //Save the changes
        //            this.unitOfWork.Save();
        //            hrm = this.Request.CreateResponse(HttpStatusCode.OK);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        hrm = this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.ToString());
        //    }
        //    return hrm;
        //}

        //[Route("Account")]
        //public HttpResponseMessage GetAccount()
        //{
        //    var accountRepo = this.unitOfWork.GetRepository<Account>();
        //    var accounts = accountRepo.Get(x => x.Type.Equals("Checking"));
        //    var result = accounts.ToList().ConvertAll(x =>
        //    {
        //        return new
        //        {
        //            AccountType = x.Type,
        //            Balance = x.Balance
        //        };
        //    });
        //    return this.Request.CreateResponse(HttpStatusCode.OK, result);
        //}

        //[Route("Account")]
        //[HttpPost]
        //public HttpResponseMessage PostAccount(AddAccountRequest request)
        //{
        //    var hrm = new HttpResponseMessage();
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(request?.UserSSN) || string.IsNullOrWhiteSpace(request?.AccountType))
        //        {
        //            this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "UserSSN and AccountType are required.");
        //        }
        //        else
        //        {
        //            var users = this.unitOfWork.GetRepository<User>();
        //            var user = users.Get(x => x.SSN == request.UserSSN).FirstOrDefault();
        //            if (user == null)
        //            {
        //                //If the user is null then return an error
        //                hrm = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No user found with matching SSN.");
        //            }
        //            else
        //            {
        //                //If the user exists then create the account
        //                var account = new Account()
        //                {
        //                    Balance = request.Balance,
        //                    Type = request.AccountType
        //                };
        //                //TODO: Refactor account to only have a single account mapping instead of a collection
        //                var accountMapping = new List<AccountMapping>()
        //                {
        //                    new AccountMapping()
        //                    {
        //                        Account = account,
        //                        User = user
        //                    }
        //                };
        //                account.AccountMappings = accountMapping;
        //                var accounts = this.unitOfWork.GetRepository<Account>();
        //                accounts.Insert(account);
        //                //Save the changes
        //                this.unitOfWork.Save();
        //                hrm = this.Request.CreateResponse(HttpStatusCode.OK);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        hrm = this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.ToString());
        //    }
        //    return hrm;
        //}

        //[Route("Accounts")]
        //public HttpResponseMessage GetAccounts()
        //{
        //    var accountMappingRepo = this.unitOfWork.GetRepository<AccountMapping>();
        //    var accounts = accountMappingRepo.Get();
        //    var result = accounts.ToList().ConvertAll(x =>
        //    {
        //        return new
        //        {
        //            FullName = $"{x.User.FirstName} {x.User.LastName}",
        //            SSN = x.User.SSN,
        //            AccountType = x.Account.Type,
        //            Balance = x.Account.Balance
        //        };
        //    });
        //    return this.Request.CreateResponse(HttpStatusCode.OK, result);
        //}
    }
}
