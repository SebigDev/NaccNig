namespace NaccNigModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PastMemberModelsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PastMembers",
                c => new
                    {
                        PastMemberId = c.String(nullable: false, maxLength: 128),
                        StateServed = c.String(),
                        Firstname = c.String(nullable: false),
                        Middlename = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Gender = c.String(),
                        Dob = c.DateTime(nullable: false),
                        StateOfOrigin = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Passport = c.Binary(),
                        PhoneNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PastMemberId);
            
            AddColumn("dbo.Donations", "PastMember_PastMemberId", c => c.String(maxLength: 128));
            AddColumn("dbo.MemberRegistrations", "PastMember_PastMemberId", c => c.String(maxLength: 128));
            AddColumn("dbo.MonthlyDues", "PastMember_PastMemberId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Donations", "PastMember_PastMemberId");
            CreateIndex("dbo.MemberRegistrations", "PastMember_PastMemberId");
            CreateIndex("dbo.MonthlyDues", "PastMember_PastMemberId");
            AddForeignKey("dbo.Donations", "PastMember_PastMemberId", "dbo.PastMembers", "PastMemberId");
            AddForeignKey("dbo.MemberRegistrations", "PastMember_PastMemberId", "dbo.PastMembers", "PastMemberId");
            AddForeignKey("dbo.MonthlyDues", "PastMember_PastMemberId", "dbo.PastMembers", "PastMemberId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MonthlyDues", "PastMember_PastMemberId", "dbo.PastMembers");
            DropForeignKey("dbo.MemberRegistrations", "PastMember_PastMemberId", "dbo.PastMembers");
            DropForeignKey("dbo.Donations", "PastMember_PastMemberId", "dbo.PastMembers");
            DropIndex("dbo.MonthlyDues", new[] { "PastMember_PastMemberId" });
            DropIndex("dbo.MemberRegistrations", new[] { "PastMember_PastMemberId" });
            DropIndex("dbo.Donations", new[] { "PastMember_PastMemberId" });
            DropColumn("dbo.MonthlyDues", "PastMember_PastMemberId");
            DropColumn("dbo.MemberRegistrations", "PastMember_PastMemberId");
            DropColumn("dbo.Donations", "PastMember_PastMemberId");
            DropTable("dbo.PastMembers");
        }
    }
}
