using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Examen_ASP.Net.Models
{
    public class Favorite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsFavorite { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }
    }
}