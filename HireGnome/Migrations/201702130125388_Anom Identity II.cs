namespace HireGnome.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnomIdentityII : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "UserId", "dbo.Users");
            DropIndex("dbo.Carts", new[] { "UserId" });
            RenameColumn(table: "dbo.Carts", name: "UserId", newName: "User_Id");
            CreateTable(
                "dbo.AnomIdentities",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        CartIt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carts", t => t.Id)
                .Index(t => t.Id);
            
            AlterColumn("dbo.Carts", "User_Id", c => c.Int());
            CreateIndex("dbo.Carts", "User_Id");
            AddForeignKey("dbo.Carts", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.AnomIdentities", "Id", "dbo.Carts");
            DropIndex("dbo.AnomIdentities", new[] { "Id" });
            DropIndex("dbo.Carts", new[] { "User_Id" });
            AlterColumn("dbo.Carts", "User_Id", c => c.Int(nullable: false));
            DropTable("dbo.AnomIdentities");
            RenameColumn(table: "dbo.Carts", name: "User_Id", newName: "UserId");
            CreateIndex("dbo.Carts", "UserId");
            AddForeignKey("dbo.Carts", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
