namespace Examen_ASP.Net.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class messagemodif : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "Product_id", c => c.Int(nullable: false));
            AddColumn("dbo.Messages", "IsRepliyed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "Answer", c => c.String());
            CreateIndex("dbo.Messages", "Product_id");
            AddForeignKey("dbo.Messages", "Product_id", "dbo.Products", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Product_id", "dbo.Products");
            DropIndex("dbo.Messages", new[] { "Product_id" });
            DropColumn("dbo.Messages", "Answer");
            DropColumn("dbo.Messages", "IsRepliyed");
            DropColumn("dbo.Messages", "Product_id");
        }
    }
}
