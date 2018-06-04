namespace NaccNig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentOptionsAdded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MembershipFees", "FeeCategory", c => c.Int(nullable: false));
            AlterColumn("dbo.RemitaPaymentLogs", "PaymentName", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RemitaPaymentLogs", "PaymentName", c => c.String());
            AlterColumn("dbo.MembershipFees", "FeeCategory", c => c.String());
        }
    }
}
