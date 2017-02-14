namespace HireGnome.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetimeonBills : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bills", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Bills", "ModificationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bills", "ModificationDate");
            DropColumn("dbo.Bills", "CreationDate");
        }
    }
}
