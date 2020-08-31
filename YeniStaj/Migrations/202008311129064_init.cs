namespace YeniStaj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Task",
                c => new
                    {
                        Taskid = c.Int(nullable: false, identity: true),
                        TaskBaslik = c.String(),
                        TaskAciklama = c.String(),
                        TaskOlusturmaTarihi = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TaskTeslimTarihi = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Projeid = c.Int(nullable: false),
                        project_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Taskid)
                .ForeignKey("dbo.Projects", t => t.project_Id)
                .Index(t => t.project_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Task", "project_Id", "dbo.Projects");
            DropIndex("dbo.Task", new[] { "project_Id" });
            DropTable("dbo.Task");
        }
    }
}
