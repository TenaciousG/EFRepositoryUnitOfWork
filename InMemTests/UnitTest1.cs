using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using System.Web.Routing;
using Effort;
using Effort.DataLoaders;
using EFRepositoryUnitOfWork.Controllers;
using EFRepositoryUnitOfWork.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InMemTests
{
    [TestClass]
    public class UnitTest1
    {
        protected internal Mock<HttpContextBase> HttpContextMock;

        [TestInitialize]
        public void TestInitialize()
        {
            var request = new Mock<HttpRequestBase>();
            // Not working - IsAjaxRequest() is static extension method and cannot be mocked
            // request.Setup(x => x.IsAjaxRequest()).Returns(true /* or false */);
            // use this
            request.SetupGet(x => x.Headers).Returns(
                new System.Net.WebHeaderCollection {
                    {"X-Requested-With", "XMLHttpRequest"}
                });

            this.HttpContextMock = new Mock<HttpContextBase>();
            this.HttpContextMock.SetupGet(x => x.Request).Returns(request.Object);

        }

        [TestMethod]
        public void TestMethod1()
        {
            //var loader = new EntityDataLoader("name=MyBankDataModel");
            //var connection = DbConnectionFactory.CreateTransient(loader);
            var connection = DbConnectionFactory.CreateTransient();
            var myInMemoryBankDataModel = new MyBankDataModel(connection);

            myInMemoryBankDataModel.MyUsers.Add(new User{FirstName = "John", LastName = "Testing"});
            myInMemoryBankDataModel.MyUsers.Add(new User{FirstName = "Knut", LastName = "Test"});
            myInMemoryBankDataModel.SaveChanges();

            var exampleController = new ExampleController();
            exampleController.Request = new HttpRequestMessage();
            exampleController.Configuration = new HttpConfiguration();
            exampleController.UnitOfWorkFactory = new UnitOfWorkFactory(myInMemoryBankDataModel);

            var getUsersHttpResponse = exampleController.GetUsersWithNameJohn();

            getUsersHttpResponse.TryGetContentValue<UserDto>(out var contentValue);
            Assert.IsTrue(contentValue.FirstName.Contains("John"));
        }
    }
}
