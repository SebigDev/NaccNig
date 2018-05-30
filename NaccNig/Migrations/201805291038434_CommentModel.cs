namespace NaccNig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CommentsArticles", "Comments_Id", "dbo.Comments");
            DropForeignKey("dbo.CommentsArticles", "Article_Id", "dbo.Articles");
            DropIndex("dbo.CommentsArticles", new[] { "Comments_Id" });
            DropIndex("dbo.CommentsArticles", new[] { "Article_Id" });
            AddColumn("dbo.Articles", "CommentId", c => c.String());
            AddColumn("dbo.Articles", "Comments_Id", c => c.Int());
            CreateIndex("dbo.Articles", "Comments_Id");
            AddForeignKey("dbo.Articles", "Comments_Id", "dbo.Comments", "Id");
            DropTable("dbo.CommentsArticles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CommentsArticles",
                c => new
                    {
                        Comments_Id = c.Int(nullable: false),
                        Article_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Comments_Id, t.Article_Id });
            
            DropForeignKey("dbo.Articles", "Comments_Id", "dbo.Comments");
            DropIndex("dbo.Articles", new[] { "Comments_Id" });
            DropColumn("dbo.Articles", "Comments_Id");
            DropColumn("dbo.Articles", "CommentId");
            CreateIndex("dbo.CommentsArticles", "Article_Id");
            CreateIndex("dbo.CommentsArticles", "Comments_Id");
            AddForeignKey("dbo.CommentsArticles", "Article_Id", "dbo.Articles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CommentsArticles", "Comments_Id", "dbo.Comments", "Id", cascadeDelete: true);
        }
    }
}
