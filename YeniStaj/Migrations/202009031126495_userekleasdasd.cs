namespace YeniStaj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userekleasdasd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Task", "Username", c => c.String());
            AddColumn("dbo.Task", "GeribildirimTarihi", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.Task", "Userid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Task", "Userid", c => c.Int(nullable: false));
            DropColumn("dbo.Task", "GeribildirimTarihi");
            DropColumn("dbo.Task", "Username");
        }
    }
}
