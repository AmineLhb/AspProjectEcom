using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Examen_ASP.Net.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        [EmailAddress]
        [System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = true)]
        [MaxLength(900)]
        public string Email { get; set; }
        [System.ComponentModel.DataAnnotations.Required]

        public String Password { get; set; }
        [System.ComponentModel.DataAnnotations.Required]

        public String Phone { get; set; }
        public string Role { get; set; }
        public string Favorite{ get; set; }
        // Navigations
        //public  Favorite Favorite { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Complaint> Complaints { get; set; }
        public ICollection<Message> SellersMessages { get; set; }
        public ICollection<Message> BuyersMessages { get; set; }


    }
}