using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using HireGnome.Models;

namespace HireGnome
{
    public class MainDbContext : DbContext // Inherited in order to load the previously installed Entity Framework
    {
        public MainDbContext() : base("name=DefaultConnection") // Here's where we link the app with the previously created database. The name of the connection may be changed in the Web.config file.
        {

        }

        public DbSet<Users> Users { get; set; } // This tells the app that is the representation of the table with its exact name
    }
}