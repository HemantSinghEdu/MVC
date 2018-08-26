using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Mvc.Login.Models
{
    public class MvcDbContext:DbContext
    {
        public MvcDbContext()
            : base("name=DefaultConnection")
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<List> List { get; set; }
    }
}