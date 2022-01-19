using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Examen_ASP.Net.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Text { get; set; }
        [ForeignKey("Seller")]
        public int Seller_id { get; set; }
        public virtual User Seller { get; set; }
        [ForeignKey("Buyer")]
        public int? Buyer_id { get; set; }
        public virtual User Buyer { get; set; }
        [ForeignKey("Product")]
        public int Product_id { get; set; }
        public Product Product { get; set; }
        public bool IsRepliyed { get; set; }
        public string Answer { get; set; }

    }
}