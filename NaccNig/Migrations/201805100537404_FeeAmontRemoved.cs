namespace NaccNigModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeeAmontRemoved : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MemberRegistrations", "FeeAmountId", "dbo.FeeAmounts");
            DropIndex("dbo.MemberRegistrations", new[] { "FeeAmountId" });
            AddColumn("dbo.Donations", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MemberRegistrations", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MonthlyDues", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.MemberRegistrations", "FeeAmountId");
            DropTable("dbo.FeeAmounts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FeeAmounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.MemberRegistrations", "FeeAmountId", c => c.Int(nullable: false));
            DropColumn("dbo.MonthlyDues", "Amount");
            DropColumn("dbo.MemberRegistrations", "Amount");
            DropColumn("dbo.Donations", "Amount");
            CreateIndex("dbo.MemberRegistrations", "FeeAmountId");
            AddForeignKey("dbo.MemberRegistrations", "FeeAmountId", "dbo.FeeAmounts", "Id", cascadeDelete: true);
        }
    }
}
