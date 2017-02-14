using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HireGnome.Models;

namespace HireGnome.ViewModels
{
    public class ShowUserBillsViewModel
    {
        public Users User { get; set; }
        public List<Bills> Bills { get; set; }
    }
}