namespace YeniStaj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taskdurumuekle1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskState",
                c => new
                    {
                        TaskStateId = c.Int(nullable: false, identity: true),
                        TaskDurumu = c.String(),
                    })
                .PrimaryKey(t => t.TaskStateId);
            
            AddColumn("dbo.Task", "TaskStateId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Task", "TaskStateId");
            DropTable("dbo.TaskState");
        }
    }
}
