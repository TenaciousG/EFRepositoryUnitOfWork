using System.Data.Common;
using System.Data.Entity;

namespace EFRepositoryUnitOfWork.Models
{
    public class MyBankDataModel : DbContext
    {
        // Your context has been configured to use a 'MyBankDataModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'EFRepositoryUnitOfWork.MyBankDataModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'MyBankDataModel' 
        // connection string in the application configuration file.
        public MyBankDataModel()
            : base("name=MyBankDataModel")
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MyBankDataModel"/> class.
        /// Only for unit tests
        /// </summary>
        /// <param name="connection">The connection.</param>
        public MyBankDataModel(DbConnection connection) : base(connection, false)
        {
            
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<User> MyUsers { get; set; }
        public virtual DbSet<Account> MyAccounts { get; set; }
        public virtual DbSet<AccountJunction> UserAndAccountJunctions { get; set; }
    }
}