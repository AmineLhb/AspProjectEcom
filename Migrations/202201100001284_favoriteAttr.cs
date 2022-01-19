namespace Examen_ASP.Net.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class favoriteAttr : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "Favorite");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Favorite", c => c.Boolean(nullable: false));
        }
    }
}
