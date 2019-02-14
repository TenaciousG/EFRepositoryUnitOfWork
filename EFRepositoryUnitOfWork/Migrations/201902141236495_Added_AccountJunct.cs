namespace EFRepositoryUnitOfWork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_AccountJunct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountJunctions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Account_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountJunctions", "User_Id", "dbo.Users");
            DropForeignKey("dbo.AccountJunctions", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.AccountJunctions", new[] { "User_Id" });
            DropIndex("dbo.AccountJunctions", new[] { "Account_Id" });
            DropTable("dbo.AccountJunctions");
        }
    }
}
