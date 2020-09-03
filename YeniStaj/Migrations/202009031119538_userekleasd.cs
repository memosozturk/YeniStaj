namespace YeniStaj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userekleasd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Task", "GeriBildirim", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Task", "GeriBildirim");
        }
    }
}
