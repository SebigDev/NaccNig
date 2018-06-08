namespace NaccNig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedZone : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ZonalExcoes", "Zone_ZId", "dbo.Zones");
            DropIndex("dbo.ZonalExcoes", new[] { "Zone_ZId" });
            AddColumn("dbo.ZonalExcoes", "ZonalName", c => c.String());
            DropColumn("dbo.ZonalExcoes", "Zone_ZId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ZonalExcoes", "Zone_ZId", c => c.Int());
            DropColumn("dbo.ZonalExcoes", "ZonalName");
            CreateIndex("dbo.ZonalExcoes", "Zone_ZId");
            AddForeignKey("dbo.ZonalExcoes", "Zone_ZId", "dbo.Zones", "ZId");
        }
    }
}
