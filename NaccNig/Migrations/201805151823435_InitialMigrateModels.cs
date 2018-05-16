namespace NaccNigModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigrateModels : DbMigration
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
                "dbo.BlogCategories",
                c => new
                    {
                        BlogCategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.BlogCategoryId);
            
            CreateTable(
                "dbo.BlogLists",
                c => new
                    {
                        BlogListId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        BlogCategoryId = c.Int(nullable: false),
                        BlogTitle = c.String(),
                    })
                .PrimaryKey(t => t.BlogListId)
                .ForeignKey("dbo.BlogCategories", t => t.BlogCategoryId, cascadeDelete: true)
                .Index(t => t.BlogCategoryId);
            
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DetailDescription = c.String(),
                        BlogListId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogLists", t => t.BlogListId, cascadeDelete: true)
                .Index(t => t.BlogListId);
            
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
                        Address = c.String(nullable: false),
                        Passport = c.Binary(),
                        PhoneNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PastMemberId);
            
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
            DropForeignKey("dbo.BlogPosts", "BlogListId", "dbo.BlogLists");
            DropForeignKey("dbo.BlogLists", "BlogCategoryId", "dbo.BlogCategories");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.BlogPosts", new[] { "BlogListId" });
            DropIndex("dbo.BlogLists", new[] { "BlogCategoryId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PastMembers");
            DropTable("dbo.ExecutiveMembers");
            DropTable("dbo.BlogPosts");
            DropTable("dbo.BlogLists");
            DropTable("dbo.BlogCategories");
            DropTable("dbo.ActiveMembers");
        }
    }
}
