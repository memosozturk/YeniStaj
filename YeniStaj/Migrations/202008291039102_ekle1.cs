namespace YeniStaj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ekle1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AvatarPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "AvatarPath");
        }
    }
}
