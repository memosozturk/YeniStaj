namespace YeniStaj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ekle3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "EklenmeTarihi", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "EklenmeTarihi", c => c.DateTime(nullable: false));
        }
    }
}
