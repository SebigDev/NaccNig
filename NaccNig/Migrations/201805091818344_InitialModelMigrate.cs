namespace NaccNigModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModelMigrate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActiveMembers",
                c => new
                    {
                        ActiveMemberId = c.String(nullable: false, maxLength: 128),
                        StateOfDeployment = c.String(),
                        StateCode = c.String(nullable: false),
                        CallUpNumber = c.String(nullable: false),
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
                .PrimaryKey(t => t.ActiveMemberId);
            
            CreateTable(
                "dbo.Donations",
                c => new
                    {
                        DonationsId = c.Int(nullable: false, identity: true),
                        IsMadeDonations = c.Boolean(nullable: false),
                        PaymentStatus = c.Int(nullable: false),
                        ActiveMemberId = c.String(maxLength: 128),
                        PaymentOptions_PaymentOptionsId = c.Int(),
                    })
                .PrimaryKey(t => t.DonationsId)
                .ForeignKey("dbo.ActiveMembers", t => t.ActiveMemberId)
                .ForeignKey("dbo.PaymentOptions", t => t.PaymentOptions_PaymentOptionsId)
                .Index(t => t.ActiveMemberId)
                .Index(t => t.PaymentOptions_PaymentOptionsId);
            
            CreateTable(
                "dbo.MemberRegistrations",
                c => new
                    {
                        MemberRegistrationId = c.Int(nullable: false, identity: true),
                        FeeAmountId = c.Int(nullable: false),
                        IsPaidRegistrationFee = c.Boolean(nullable: false),
                        PaymentStatus = c.Int(nullable: false),
                        ActiveMemberId = c.String(maxLength: 128),
                        PaymentOptions_PaymentOptionsId = c.Int(),
                    })
                .PrimaryKey(t => t.MemberRegistrationId)
                .ForeignKey("dbo.ActiveMembers", t => t.ActiveMemberId)
                .ForeignKey("dbo.FeeAmounts", t => t.FeeAmountId, cascadeDelete: true)
                .ForeignKey("dbo.PaymentOptions", t => t.PaymentOptions_PaymentOptionsId)
                .Index(t => t.FeeAmountId)
                .Index(t => t.ActiveMemberId)
                .Index(t => t.PaymentOptions_PaymentOptionsId);
            
            CreateTable(
                "dbo.FeeAmounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MonthlyDues",
                c => new
                    {
                        MonthlyDuesId = c.Int(nullable: false, identity: true),
                        IsPaidMonthlyDues = c.Boolean(nullable: false),
                        PaymentStatus = c.Int(nullable: false),
                        ActiveMemberId = c.String(maxLength: 128),
                        PaymentOptions_PaymentOptionsId = c.Int(),
                    })
                .PrimaryKey(t => t.MonthlyDuesId)
                .ForeignKey("dbo.ActiveMembers", t => t.ActiveMemberId)
                .ForeignKey("dbo.PaymentOptions", t => t.PaymentOptions_PaymentOptionsId)
                .Index(t => t.ActiveMemberId)
                .Index(t => t.PaymentOptions_PaymentOptionsId);
            
            CreateTable(
                "dbo.ExecutiveMembers",
                c => new
                    {
                        ExecutiveMemberId = c.String(nullable: false, maxLength: 128),
                        Position = c.String(),
                    })
                .PrimaryKey(t => t.ExecutiveMemberId);
            
            CreateTable(
                "dbo.PaymentOptions",
                c => new
                    {
                        PaymentOptionsId = c.Int(nullable: false, identity: true),
                        PaymentOptionsName = c.Int(nullable: false),
                        ActiveMemberId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PaymentOptionsId)
                .ForeignKey("dbo.ActiveMembers", t => t.ActiveMemberId)
                .Index(t => t.ActiveMemberId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.MonthlyDues", "PaymentOptions_PaymentOptionsId", "dbo.PaymentOptions");
            DropForeignKey("dbo.MemberRegistrations", "PaymentOptions_PaymentOptionsId", "dbo.PaymentOptions");
            DropForeignKey("dbo.Donations", "PaymentOptions_PaymentOptionsId", "dbo.PaymentOptions");
            DropForeignKey("dbo.PaymentOptions", "ActiveMemberId", "dbo.ActiveMembers");
            DropForeignKey("dbo.MonthlyDues", "ActiveMemberId", "dbo.ActiveMembers");
            DropForeignKey("dbo.MemberRegistrations", "FeeAmountId", "dbo.FeeAmounts");
            DropForeignKey("dbo.MemberRegistrations", "ActiveMemberId", "dbo.ActiveMembers");
            DropForeignKey("dbo.Donations", "ActiveMemberId", "dbo.ActiveMembers");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PaymentOptions", new[] { "ActiveMemberId" });
            DropIndex("dbo.MonthlyDues", new[] { "PaymentOptions_PaymentOptionsId" });
            DropIndex("dbo.MonthlyDues", new[] { "ActiveMemberId" });
            DropIndex("dbo.MemberRegistrations", new[] { "PaymentOptions_PaymentOptionsId" });
            DropIndex("dbo.MemberRegistrations", new[] { "ActiveMemberId" });
            DropIndex("dbo.MemberRegistrations", new[] { "FeeAmountId" });
            DropIndex("dbo.Donations", new[] { "PaymentOptions_PaymentOptionsId" });
            DropIndex("dbo.Donations", new[] { "ActiveMemberId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PaymentOptions");
            DropTable("dbo.ExecutiveMembers");
            DropTable("dbo.MonthlyDues");
            DropTable("dbo.FeeAmounts");
            DropTable("dbo.MemberRegistrations");
            DropTable("dbo.Donations");
            DropTable("dbo.ActiveMembers");
        }
    }
}
