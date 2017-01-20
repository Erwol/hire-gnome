namespace HireGnome.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Name = c.String(),
                        Public = c.String(),
                        Billed = c.String(),
                        MainCart = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Details = c.String(nullable: false),
                        Public = c.String(),
                        Name = c.String(nullable: false),
                        AddedDate = c.String(),
                        ModifiedDate = c.String(),
                        Price = c.Double(nullable: false),
                        Offer = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Name = c.String(),
                        Country = c.String(),
                        RolId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RolId, cascadeDelete: true)
                .Index(t => t.RolId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rol = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            Sql("INSERT INTO Roles (Rol, Description) VALUES ('admin', 'Admin rol')");
            Sql("INSERT INTO Roles (Rol, Description) VALUES ('user', 'User rol')");
            Sql("INSERT INTO Users (Email, Password, Name, Country, RolId) VALUES ('admin@hiregnome.com', 'admin', 'admin', 'Spain', 1)");

            CreateTable(
                "dbo.Lists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Details = c.String(),
                        Date_Posted = c.String(),
                        Time_Posted = c.String(),
                        Date_Edited = c.String(),
                        Time_Edited = c.String(),
                        Public = c.String(),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CartsProducts",
                c => new
                    {
                        Carts_Id = c.Int(nullable: false),
                        Products_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Carts_Id, t.Products_Id })
                .ForeignKey("dbo.Carts", t => t.Carts_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Products_Id, cascadeDelete: true)
                .Index(t => t.Carts_Id)
                .Index(t => t.Products_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RolId", "dbo.Roles");
            DropForeignKey("dbo.CartsProducts", "Products_Id", "dbo.Products");
            DropForeignKey("dbo.CartsProducts", "Carts_Id", "dbo.Carts");
            DropIndex("dbo.CartsProducts", new[] { "Products_Id" });
            DropIndex("dbo.CartsProducts", new[] { "Carts_Id" });
            DropIndex("dbo.Users", new[] { "RolId" });
            DropIndex("dbo.Carts", new[] { "UserId" });
            DropTable("dbo.CartsProducts");
            DropTable("dbo.Lists");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.Carts");
        }
    }
}
