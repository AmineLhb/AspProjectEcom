namespace Examen_ASP.Net.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFavoriteAttr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Favorite", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Favorite");
        }
    }
}
