using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Examen_ASP.Net.Models
{
    public class Complaint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        [ForeignKey("User")]
        public int User_id { get; set; }
        //Navigation
        public User User { get; set; } 

    }
}