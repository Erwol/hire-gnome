using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HireGnome.ViewModels
{
    public class ListGnomesViewModel
    {
        public virtual ICollection<ShowGnomeViewModel> Gnomes { get; set; }
    }
}