namespace Examen_ASP.Net.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loginrequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Phone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Phone", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Name", c => c.String());
        }
    }
}
