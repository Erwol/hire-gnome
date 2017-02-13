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
        [Key]  
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)] // Masked field
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public string Country { get; set; }

        // One to many relation with roles
        public int RolId { get; set; }
        [ForeignKey("RolId")]
        public virtual Roles Rol { get; set; }

        // All the carts that the user is managing (such a whishes list, etc)
        
        public virtual ICollection<Carts> Carts { get; set; }

        // The bills of the user
        //public virtual ICollection<Bills> Bills { get; set; }

        public bool IsActive { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ModificationDate { get; set; }

        public Users()
        {
            IsActive = false;

            CreationDate = DateTime.Now;
            ModificationDate = DateTime.Now;
        }
    }
}