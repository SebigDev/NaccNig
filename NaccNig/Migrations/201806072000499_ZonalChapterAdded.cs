namespace NaccNig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ZonalChapterAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ZonalChapters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZoneName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ZonalExcoes", "ZoneId", c => c.Int(nullable: false));
            AddColumn("dbo.ZonalExcoes", "ZonalChapter_Id", c => c.Int());
            CreateIndex("dbo.ZonalExcoes", "ZonalChapter_Id");
            AddForeignKey("dbo.ZonalExcoes", "ZonalChapter_Id", "dbo.ZonalChapters", "Id");
            DropColumn("dbo.ZonalExcoes", "ZonalName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ZonalExcoes", "ZonalName", c => c.String());
            DropForeignKey("dbo.ZonalExcoes", "ZonalChapter_Id", "dbo.ZonalChapters");
            DropIndex("dbo.ZonalExcoes", new[] { "ZonalChapter_Id" });
            DropColumn("dbo.ZonalExcoes", "ZonalChapter_Id");
            DropColumn("dbo.ZonalExcoes", "ZoneId");
            DropTable("dbo.ZonalChapters");
        }
    }
}
