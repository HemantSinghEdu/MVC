using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mvc.CRUD.Models
{
    public class MvcDbContext: DbContext
    {
        public MvcDbContext():
            base("name=DefaultConnection")
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Lists> Lists { get; set; }
    }
}