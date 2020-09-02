namespace YeniStaj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taskdurumuekle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Task", "TaskDurumu", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Task", "TaskDurumu");
        }
    }
}
