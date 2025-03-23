namespace CommonPackages.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUserAndUserAddressesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User_Addresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        City = c.String(maxLength: 255),
                        Address = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_Addresses", "UserID", "dbo.Users");
            DropIndex("dbo.User_Addresses", new[] { "UserID" });
            DropTable("dbo.Users");
            DropTable("dbo.User_Addresses");
        }
    }
}
