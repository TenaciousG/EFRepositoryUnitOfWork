namespace EFRepositoryUnitOfWork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_AccountNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "AccountNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "AccountNumber");
        }
    }
}
