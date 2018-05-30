namespace NaccNig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentDetail = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CommentsArticles",
                c => new
                    {
                        Comments_Id = c.Int(nullable: false),
                        Article_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Comments_Id, t.Article_Id })
                .ForeignKey("dbo.Comments", t => t.Comments_Id, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.Article_Id, cascadeDelete: true)
                .Index(t => t.Comments_Id)
                .Index(t => t.Article_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommentsArticles", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.CommentsArticles", "Comments_Id", "dbo.Comments");
            DropIndex("dbo.CommentsArticles", new[] { "Article_Id" });
            DropIndex("dbo.CommentsArticles", new[] { "Comments_Id" });
            DropTable("dbo.CommentsArticles");
            DropTable("dbo.Comments");
        }
    }
}
