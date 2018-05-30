namespace NaccNig.Migrations
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
                        StateCode = c.String(nullable: false),
                        CallUpNumber = c.String(nullable: false),
                        Firstname = c.String(nullable: false),
                        Middlename = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Gender = c.String(),
                        Dob = c.DateTime(nullable: false),
                        StateOfOrigin = c.String(nullable: false),
                        StateChapterId = c.Int(nullable: false),
                        ZoneId = c.Int(nullable: false),
                        Address = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Photo = c.String(),
                        IsExco = c.Boolean(nullable: false),
                        Position = c.String(),
                        DateServed = c.String(),
                        StateChapter_StateChapId = c.Int(),
                        Zone_ZId = c.Int(),
                    })
                .PrimaryKey(t => t.ActiveMemberId)
                .ForeignKey("dbo.StateChapters", t => t.StateChapter_StateChapId)
                .ForeignKey("dbo.Zones", t => t.Zone_ZId)
                .Index(t => t.StateChapter_StateChapId)
                .Index(t => t.Zone_ZId);
            
            CreateTable(
                "dbo.StateChapters",
                c => new
                    {
                        StateChapId = c.Int(nullable: false, identity: true),
                        StateChapterName = c.String(),
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
                "dbo.Amounts",
                c => new
                    {
                        PriceId = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        PaymentCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PriceId);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Content = c.String(),
                        AuthorId = c.String(maxLength: 128),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.CategoryId);
            
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
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        StateChapterId = c.Int(nullable: false),
                        ZoneId = c.Int(nullable: false),
                        Address = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Photo = c.String(),
                        IsExco = c.Boolean(nullable: false),
                        Position = c.String(),
                        DateServed = c.String(),
                        StateChapter_StateChapId = c.Int(),
                        Zone_ZId = c.Int(),
                    })
                .PrimaryKey(t => t.PastMemberId)
                .ForeignKey("dbo.StateChapters", t => t.StateChapter_StateChapId)
                .ForeignKey("dbo.Zones", t => t.Zone_ZId)
                .Index(t => t.StateChapter_StateChapId)
                .Index(t => t.Zone_ZId);
            
            CreateTable(
                "dbo.PaymentCategories",
                c => new
                    {
                        PaymentCategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.PaymentCategoryId);
            
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
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.ActiveMembers", t => t.ActiveMemberId)
                .ForeignKey("dbo.Amounts", t => t.Amount_PriceId)
                .Index(t => t.ActiveMemberId)
                .Index(t => t.Amount_PriceId);
            
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
                "dbo.TagArticles",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Article_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Article_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.Article_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Article_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PaymentSettings", "Amount_PriceId", "dbo.Amounts");
            DropForeignKey("dbo.PaymentSettings", "ActiveMemberId", "dbo.ActiveMembers");
            DropForeignKey("dbo.PastMembers", "Zone_ZId", "dbo.Zones");
            DropForeignKey("dbo.PastMembers", "StateChapter_StateChapId", "dbo.StateChapters");
            DropForeignKey("dbo.TagArticles", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.TagArticles", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.Articles", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Articles", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ActiveMembers", "Zone_ZId", "dbo.Zones");
            DropForeignKey("dbo.ActiveMembers", "StateChapter_StateChapId", "dbo.StateChapters");
            DropIndex("dbo.TagArticles", new[] { "Article_Id" });
            DropIndex("dbo.TagArticles", new[] { "Tag_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PaymentSettings", new[] { "Amount_PriceId" });
            DropIndex("dbo.PaymentSettings", new[] { "ActiveMemberId" });
            DropIndex("dbo.PastMembers", new[] { "Zone_ZId" });
            DropIndex("dbo.PastMembers", new[] { "StateChapter_StateChapId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Articles", new[] { "CategoryId" });
            DropIndex("dbo.Articles", new[] { "AuthorId" });
            DropIndex("dbo.ActiveMembers", new[] { "Zone_ZId" });
            DropIndex("dbo.ActiveMembers", new[] { "StateChapter_StateChapId" });
            DropTable("dbo.TagArticles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PaymentSettings");
            DropTable("dbo.PaymentCategories");
            DropTable("dbo.PastMembers");
            DropTable("dbo.ExecutiveMembers");
            DropTable("dbo.Tags");
            DropTable("dbo.Categories");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Articles");
            DropTable("dbo.Amounts");
            DropTable("dbo.Zones");
            DropTable("dbo.StateChapters");
            DropTable("dbo.ActiveMembers");
        }
    }
}
