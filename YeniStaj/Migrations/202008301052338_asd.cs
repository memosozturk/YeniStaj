namespace YeniStaj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Projects", "UpdatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "UpdatedDate");
            DropColumn("dbo.Projects", "CreatedDate");
        }
    }
}
