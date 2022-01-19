namespace Examen_ASP.Net.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Discount = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Address = c.String(),
                        Status = c.Boolean(nullable: false),
                        Category_id = c.Int(nullable: false),
                        User_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: true)
                .Index(t => t.Category_id)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Product_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_id, cascadeDelete: true)
                .Index(t => t.Product_id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(maxLength: 900),
                        Password = c.String(),
                        Phone = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Seller_id = c.Int(nullable: false),
                        Buyer_id = c.Int(),
                        User_Id = c.Int(),
                        User_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Buyer_id)
                .ForeignKey("dbo.Users", t => t.Seller_id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.User_Id1)
                .Index(t => t.Seller_id)
                .Index(t => t.Buyer_id)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1);
            
            CreateTable(
                "dbo.Complaints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        Answer = c.String(),
                        User_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: true)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsFavorite = c.Boolean(nullable: false),
                        User_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: true)
                .Index(t => t.User_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "User_id", "dbo.Users");
            DropForeignKey("dbo.Messages", "User_Id1", "dbo.Users");
            DropForeignKey("dbo.Favorites", "User_id", "dbo.Users");
            DropForeignKey("dbo.Complaints", "User_id", "dbo.Users");
            DropForeignKey("dbo.Messages", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "Seller_id", "dbo.Users");
            DropForeignKey("dbo.Messages", "Buyer_id", "dbo.Users");
            DropForeignKey("dbo.Images", "Product_id", "dbo.Products");
            DropForeignKey("dbo.Products", "Category_id", "dbo.Categories");
            DropIndex("dbo.Favorites", new[] { "User_id" });
            DropIndex("dbo.Complaints", new[] { "User_id" });
            DropIndex("dbo.Messages", new[] { "User_Id1" });
            DropIndex("dbo.Messages", new[] { "User_Id" });
            DropIndex("dbo.Messages", new[] { "Buyer_id" });
            DropIndex("dbo.Messages", new[] { "Seller_id" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Images", new[] { "Product_id" });
            DropIndex("dbo.Products", new[] { "User_id" });
            DropIndex("dbo.Products", new[] { "Category_id" });
            DropTable("dbo.Favorites");
            DropTable("dbo.Complaints");
            DropTable("dbo.Messages");
            DropTable("dbo.Users");
            DropTable("dbo.Images");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
