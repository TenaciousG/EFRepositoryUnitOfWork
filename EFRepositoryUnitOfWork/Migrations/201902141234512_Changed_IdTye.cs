namespace EFRepositoryUnitOfWork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changed_IdTye : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Accounts");
            AlterColumn("dbo.Accounts", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Accounts", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Accounts");
            AlterColumn("dbo.Accounts", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Accounts", "Id");
        }
    }
}
