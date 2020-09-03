namespace YeniStaj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userekle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Task", "Userid", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Task", "Userid");
        }
    }
}
