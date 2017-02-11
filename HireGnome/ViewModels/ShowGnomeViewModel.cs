using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireGnome.ViewModels
{
    public class ShowGnomeViewModel
    {
        [Required]
        [Display(Name = "Name of this Gnome")]
        public string Name { get; set; }

        [Key]
        [Required]
        public int Id { get; set; }
        [Display(Name = "Some interesting details")]
        public string Details { get; set; }

        [Display(Name = "Make the Gnome public?")]
        public bool IsPublic { get; set; }

        // TODO: Add Image model and link here
        [Display(Name = "Link to the Gnome Image")]
        public string Image { get; set; }

        [Display(Name = "Latitude")]
        public double Latitude { get; set; }

        [Display(Name = "Longitude")]
        public double Longitude { get; set; }

        [Display(Name = "Price")]
        [Required]
        public double Price { get; set; }

        public double ReducedPrice { get; set; }

        [Display(Name = "Offer, from 0% to 100%")]
        [Range(0, 100)]
        public int Offer { get; set; }
    }
}