namespace NaccNig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOnPayCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipFees", "PaymentMode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MembershipFees", "PaymentMode");
        }
    }
}
