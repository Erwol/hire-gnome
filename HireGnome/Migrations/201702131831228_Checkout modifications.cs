namespace HireGnome.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Checkoutmodifications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FinalPrice = c.Double(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carts", t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Users", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Country", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bills", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Bills", "Id", "dbo.Carts");
            DropIndex("dbo.Bills", new[] { "User_Id" });
            DropIndex("dbo.Bills", new[] { "Id" });
            AlterColumn("dbo.Users", "Country", c => c.String());
            DropColumn("dbo.Users", "Address");
            DropTable("dbo.Bills");
        }
    }
}
