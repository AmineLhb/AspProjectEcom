using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Examen_ASP.Net.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public String Address { get; set; }
        public bool Status { get; set; }
        [ForeignKey("Category")]
        public int Category_id { get; set; }
        public Category Category { get; set; }
        [ForeignKey("User")]
        public int User_id { get; set; }
        public User User { get; set; }
        public ICollection<Image>Images { get; set; }

    }
}