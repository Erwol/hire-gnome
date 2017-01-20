using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace HireGnome.Models
{
    public class Roles
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Rol { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}