namespace NaccNig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExcecutivesModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Portfolios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PortfolioName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StateExcoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateExcoId = c.Int(nullable: false),
                        PortfolioId = c.Int(nullable: false),
                        ActiveMemberId = c.String(maxLength: 128),
                        StateChapter_StateChapId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ActiveMembers", t => t.ActiveMemberId)
                .ForeignKey("dbo.Portfolios", t => t.PortfolioId, cascadeDelete: true)
                .ForeignKey("dbo.StateChapters", t => t.StateChapter_StateChapId)
                .Index(t => t.PortfolioId)
                .Index(t => t.ActiveMemberId)
                .Index(t => t.StateChapter_StateChapId);
            
            CreateTable(
                "dbo.ZonalExcoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZonalExcoId = c.Int(nullable: false),
                        PortfolioId = c.Int(nullable: false),
                        ActiveMemberId = c.String(maxLength: 128),
                        Zone_ZId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ActiveMembers", t => t.ActiveMemberId)
                .ForeignKey("dbo.Portfolios", t => t.PortfolioId, cascadeDelete: true)
                .ForeignKey("dbo.Zones", t => t.Zone_ZId)
                .Index(t => t.PortfolioId)
                .Index(t => t.ActiveMemberId)
                .Index(t => t.Zone_ZId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ZonalExcoes", "Zone_ZId", "dbo.Zones");
            DropForeignKey("dbo.ZonalExcoes", "PortfolioId", "dbo.Portfolios");
            DropForeignKey("dbo.ZonalExcoes", "ActiveMemberId", "dbo.ActiveMembers");
            DropForeignKey("dbo.StateExcoes", "StateChapter_StateChapId", "dbo.StateChapters");
            DropForeignKey("dbo.StateExcoes", "PortfolioId", "dbo.Portfolios");
            DropForeignKey("dbo.StateExcoes", "ActiveMemberId", "dbo.ActiveMembers");
            DropIndex("dbo.ZonalExcoes", new[] { "Zone_ZId" });
            DropIndex("dbo.ZonalExcoes", new[] { "ActiveMemberId" });
            DropIndex("dbo.ZonalExcoes", new[] { "PortfolioId" });
            DropIndex("dbo.StateExcoes", new[] { "StateChapter_StateChapId" });
            DropIndex("dbo.StateExcoes", new[] { "ActiveMemberId" });
            DropIndex("dbo.StateExcoes", new[] { "PortfolioId" });
            DropTable("dbo.ZonalExcoes");
            DropTable("dbo.StateExcoes");
            DropTable("dbo.Portfolios");
        }
    }
}
