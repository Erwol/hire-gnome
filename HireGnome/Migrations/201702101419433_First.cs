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
                        IsPublic = c.Boolean(nullable: false),
                        IsBilled = c.Boolean(nullable: false),
                        IsMainCart = c.Boolean(nullable: false),
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
                        Name = c.String(nullable: false),
                        Details = c.String(),
                        IsPublic = c.Boolean(nullable: false),
                        Image = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        Offer = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        FirstName = c.String(),
                        SecondName = c.String(),
                        Country = c.String(),
                        RolId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: true),
                        ModificationDate = c.DateTime(nullable: true),
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
            
            CreateTable(
                "dbo.Names",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        surname = c.String(),
                        phone = c.Int(nullable: false),
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

            Sql(HireGnome.Properties.Resources.initial_data);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RolId", "dbo.Roles");
            DropForeignKey("dbo.Carts", "UserId", "dbo.Users");
            DropForeignKey("dbo.CartsProducts", "Products_Id", "dbo.Products");
            DropForeignKey("dbo.CartsProducts", "Carts_Id", "dbo.Carts");
            DropIndex("dbo.CartsProducts", new[] { "Products_Id" });
            DropIndex("dbo.CartsProducts", new[] { "Carts_Id" });
            DropIndex("dbo.Users", new[] { "RolId" });
            DropIndex("dbo.Carts", new[] { "UserId" });
            DropTable("dbo.CartsProducts");
            DropTable("dbo.Names");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.Carts");
        }
    }
}
