using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc.CRUD.Models
{
    /*
     *  CREATE TABLE [dbo].[List]
        (
	        [Id] INT NOT NULL PRIMARY KEY IDENTITY(0,1),
	        [Details] NVARCHAR(100) NOT NULL,
	        [Date_Posted] NVARCHAR(30) NULL,
	        [Time_Posted] NVARCHAR(50) NULL,
	        [Date_Edited] NVARCHAR(30) NULL,
	        [Time_Edited] NVARCHAR(50) NULL,
	        [public] NVARCHAR(5) NOT NULL,
	        [User_Id] INT NOT NULL
        )
     */
    public class Lists
    {
        [Key]
        public int Id { get; set; }
        public string Details { get; set; }
        public string Date_Posted { get; set; }
        public string Time_Posted { get; set; }
        public string Date_Edited { get; set; }
        public string Time_Edited { get; set; }
        public string Public { get; set; }
        public int User_Id { get; set; }
    }
}