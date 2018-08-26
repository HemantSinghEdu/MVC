using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Login.Models
{
    /*
     *     
    [Id]       INT            IDENTITY (0, 1) NOT NULL,
    [Email]    NVARCHAR (50)  NOT NULL,
    [Password] NVARCHAR (100) NOT NULL,
    [Name]     NVARCHAR (100) NOT NULL,
    [Country]  NVARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
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