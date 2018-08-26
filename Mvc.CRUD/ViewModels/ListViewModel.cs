using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc.CRUD.ViewModels
{
    public class ListViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Details { get; set; }
        public string Date_Posted { get; set; }
        public string Time_Posted { get; set; }
        public string Date_Edited { get; set; }
        public string Time_Edited { get; set; }
        public bool Public { get; set; }
        public string StrPublic {
            get 
            { 
                return Public ? "Yes" : "No"; 
            }
        }
    }
}