using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Examen_ASP.Net.Data
{
    public class Examen_ASPNetContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Examen_ASPNetContext() : base("name=Examen_ASPNetContext")
        {
        }

        public System.Data.Entity.DbSet<Examen_ASP.Net.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<Examen_ASP.Net.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Examen_ASP.Net.Models.Complaint> Complaints { get; set; }

        public System.Data.Entity.DbSet<Examen_ASP.Net.Models.Image> Images { get; set; }

        public System.Data.Entity.DbSet<Examen_ASP.Net.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<Examen_ASP.Net.Models.Favorite> Favorites { get; set; }

        public System.Data.Entity.DbSet<Examen_ASP.Net.Models.Message> Messages { get; set; }
    }
}
