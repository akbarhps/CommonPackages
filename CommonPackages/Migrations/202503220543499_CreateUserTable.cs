namespace CommonPackages.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUserTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAddresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        City = c.String(maxLength: 2147483647),
                        Address = c.String(maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAddresses", "UserID", "dbo.Users");
            DropIndex("dbo.UserAddresses", new[] { "UserID" });
            DropTable("dbo.Users");
            DropTable("dbo.UserAddresses");
        }
    }
}
