using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Examen_ASP.Net.Models
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Path { get; set; }

        [ForeignKey("Product")]
        public int Product_id { get; set; }
        //Navigation
        public Product Product { get; set; }



    }
}