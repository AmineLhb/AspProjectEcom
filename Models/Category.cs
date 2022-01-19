using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Examen_ASP.Net.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {  get; set; }
        public string Name { get; set; }

        //Navigation
        public ICollection<Product> Products {  get; set;}

    }
}