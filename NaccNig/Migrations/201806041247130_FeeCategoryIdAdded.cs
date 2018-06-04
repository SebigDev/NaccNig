namespace NaccNig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeeCategoryIdAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberFeeTypes", "FeeCategoryId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemberFeeTypes", "FeeCategoryId");
        }
    }
}
