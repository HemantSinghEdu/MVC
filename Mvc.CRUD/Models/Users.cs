using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc.CRUD.Models
{
    /*
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(0,1),
	[Email] NVARCHAR(50) NOT NULL,
	[Password] NVARCHAR(50) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[Country] NVARCHAR(50) NOT NULL 
     */
    
    public class Users
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }
    }
}