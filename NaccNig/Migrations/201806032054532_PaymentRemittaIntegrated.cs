namespace NaccNig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentRemittaIntegrated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PaymentSettings", "ActiveMemberId", "dbo.ActiveMembers");
            DropForeignKey("dbo.PaymentSettings", "Amount_PriceId", "dbo.Amounts");
            DropIndex("dbo.PaymentSettings", new[] { "ActiveMemberId" });
            DropIndex("dbo.PaymentSettings", new[] { "Amount_PriceId" });
            CreateTable(
                "dbo.MembershipFees",
                c => new
                    {
                        MembershipFeeId = c.Int(nullable: false, identity: true),
                        ActiveMemberId = c.String(nullable: false, maxLength: 128),
                        ReferenceNo = c.String(),
                        OrderId = c.String(nullable: false, maxLength: 50),
                        FeeCategory = c.String(),
                        FeeName = c.String(),
                        Description = c.String(),
                        PaidFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        PaymentStatus = c.String(),
                    })
                .PrimaryKey(t => t.MembershipFeeId)
                .ForeignKey("dbo.ActiveMembers", t => t.ActiveMemberId, cascadeDelete: true)
                .Index(t => t.ActiveMemberId);
            
            CreateTable(
                "dbo.RemitaPaymentLogs",
                c => new
                    {
                        RemitaPaymentLogId = c.Int(nullable: false, identity: true),
                        OrderId = c.String(),
                        Amount = c.String(),
                        Rrr = c.String(),
                        StatusCode = c.String(),
                        TransactionMessage = c.String(),
                        PaymentDate = c.DateTime(nullable: false),
                        PaymentName = c.String(),
                        PayerName = c.String(),
                    })
                .PrimaryKey(t => t.RemitaPaymentLogId);
            
            DropTable("dbo.Amounts");
            DropTable("dbo.PaymentCategories");
            DropTable("dbo.PaymentSettings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PaymentSettings",
                c => new
                    {
                        PaymentId = c.String(nullable: false, maxLength: 128),
                        PaymentCategoryId = c.Int(nullable: false),
                        AmountId = c.Int(nullable: false),
                        ActiveMemberId = c.String(maxLength: 128),
                        Amount_PriceId = c.Int(),
                    })
                .PrimaryKey(t => t.PaymentId);
            
            CreateTable(
                "dbo.PaymentCategories",
                c => new
                    {
                        PaymentCategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.PaymentCategoryId);
            
            CreateTable(
                "dbo.Amounts",
                c => new
                    {
                        PriceId = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        PaymentCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PriceId);
            
            DropForeignKey("dbo.MembershipFees", "ActiveMemberId", "dbo.ActiveMembers");
            DropIndex("dbo.MembershipFees", new[] { "ActiveMemberId" });
            DropTable("dbo.RemitaPaymentLogs");
            DropTable("dbo.MembershipFees");
            CreateIndex("dbo.PaymentSettings", "Amount_PriceId");
            CreateIndex("dbo.PaymentSettings", "ActiveMemberId");
            AddForeignKey("dbo.PaymentSettings", "Amount_PriceId", "dbo.Amounts", "PriceId");
            AddForeignKey("dbo.PaymentSettings", "ActiveMemberId", "dbo.ActiveMembers", "ActiveMemberId");
        }
    }
}
