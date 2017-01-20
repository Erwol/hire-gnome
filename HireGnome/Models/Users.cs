using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireGnome.Models
{
    public class Users
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]  
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)] // Masked field
        public string Password { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        // One to many relation with roles
        public int RolId { get; set; }
        [ForeignKey("RolId")]
        public virtual Roles Rol { get; set; }

        // The bills of the user
        //public virtual ICollection<Bills> Bills { get; set; }
    }
}