using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Login.Models
{
    /*
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(0,1),
	[Details] NVARCHAR(100) NOT NULL,
	[Date_Posted] NVARCHAR(30) NOT NULL,
	[Time_Posted] NVARCHAR(50) NOT NULL,
	[Date_Edited] NVARCHAR(30) NOT NULL,
	[Time_Edited] NVARCHAR(50) NOT NULL,
	[public] NVARCHAR(5) NOT NULL
     */
    public class List
    {
        [Key]
        public int Id { get; set; }

        public string Details { get; set; }

        public string Date_Posted { get; set; }

        public string Time_Posted { get; set; }

        public string Date_Edited { get; set; }

        public string Time_Edited { get; set; }

        public string Public { get; set; }

    }
}