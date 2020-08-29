namespace YeniStaj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ekle4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Projeid", c => c.Int());
            AddColumn("dbo.AspNetUsers", "ProjectUser_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "ProjectUser_Id");
            AddForeignKey("dbo.AspNetUsers", "ProjectUser_Id", "dbo.Projects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "ProjectUser_Id", "dbo.Projects");
            DropIndex("dbo.AspNetUsers", new[] { "ProjectUser_Id" });
            DropColumn("dbo.AspNetUsers", "ProjectUser_Id");
            DropColumn("dbo.AspNetUsers", "Projeid");
        }
    }
}
