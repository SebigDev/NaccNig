namespace NaccNig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentIntegrated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MemberFeeTypes", "FeeCategory", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MemberFeeTypes", "FeeCategory", c => c.String());
        }
    }
}
