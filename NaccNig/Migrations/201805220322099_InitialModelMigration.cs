namespace NaccNigModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModelMigration : DbMigration
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
                        ProvinceId = c.Int(nullable: false),
                        StateChapterId = c.Int(nullable: false),
                        ZoneId = c.Int(nullable: false),
                        Address = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Photo = c.String(),
                        Province_ProId = c.Int(),
                        StateChapter_StateChapId = c.Int(),
                        Zone_ZId = c.Int(),
                    })
                .PrimaryKey(t => t.ActiveMemberId)
                .ForeignKey("dbo.Provinces", t => t.Province_ProId)
                .ForeignKey("dbo.StateChapters", t => t.StateChapter_StateChapId)
                .ForeignKey("dbo.Zones", t => t.Zone_ZId)
                .Index(t => t.Province_ProId)
                .Index(t => t.StateChapter_StateChapId)
                .Index(t => t.Zone_ZId);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        ProId = c.Int(nullable: false, identity: true),
                        ProvinceName = c.String(),
                    })
                .PrimaryKey(t => t.ProId);
            
            CreateTable(
                "dbo.StateChapters",
                c => new
                    {
                        StateChapId = c.Int(nullable: false, identity: true),
                        StateChapterName = c.String(),
                        ProId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StateChapId);
            
            CreateTable(
                "dbo.Zones",
                c => new
                    {
                        ZId = c.Int(nullable: false, identity: true),
                        ZoneName = c.String(),
                        StateChapId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ZId);
            
            CreateTable(
                "dbo.ExecutiveMembers",
                c => new
                    {
                        ExecutiveMemberId = c.String(nullable: false, maxLength: 128),
                        Position = c.String(),
                    })
                .PrimaryKey(t => t.ExecutiveMemberId);
            
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
                        ProvinceId = c.Int(nullable: false),
                        StateChapterId = c.Int(nullable: false),
                        ZoneId = c.Int(nullable: false),
                        Address = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Photo = c.String(),
                        Province_ProId = c.Int(),
                        StateChapter_StateChapId = c.Int(),
                        Zone_ZId = c.Int(),
                    })
                .PrimaryKey(t => t.PastMemberId)
                .ForeignKey("dbo.Provinces", t => t.Province_ProId)
                .ForeignKey("dbo.StateChapters", t => t.StateChapter_StateChapId)
                .ForeignKey("dbo.Zones", t => t.Zone_ZId)
                .Index(t => t.Province_ProId)
                .Index(t => t.StateChapter_StateChapId)
                .Index(t => t.Zone_ZId);
            
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
            DropForeignKey("dbo.PastMembers", "Zone_ZId", "dbo.Zones");
            DropForeignKey("dbo.PastMembers", "StateChapter_StateChapId", "dbo.StateChapters");
            DropForeignKey("dbo.PastMembers", "Province_ProId", "dbo.Provinces");
            DropForeignKey("dbo.ActiveMembers", "Zone_ZId", "dbo.Zones");
            DropForeignKey("dbo.ActiveMembers", "StateChapter_StateChapId", "dbo.StateChapters");
            DropForeignKey("dbo.ActiveMembers", "Province_ProId", "dbo.Provinces");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PastMembers", new[] { "Zone_ZId" });
            DropIndex("dbo.PastMembers", new[] { "StateChapter_StateChapId" });
            DropIndex("dbo.PastMembers", new[] { "Province_ProId" });
            DropIndex("dbo.ActiveMembers", new[] { "Zone_ZId" });
            DropIndex("dbo.ActiveMembers", new[] { "StateChapter_StateChapId" });
            DropIndex("dbo.ActiveMembers", new[] { "Province_ProId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PastMembers");
            DropTable("dbo.ExecutiveMembers");
            DropTable("dbo.Zones");
            DropTable("dbo.StateChapters");
            DropTable("dbo.Provinces");
            DropTable("dbo.ActiveMembers");
        }
    }
}
