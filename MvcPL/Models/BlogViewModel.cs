using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        [Display(Name="Name of the blog")]
        public string BlogName { get; set; }
        public string Description { get; set; }
        [Display(Name = "Time of creation")]
        public DateTime CreateTime { get; set; }

        public int UserId { get; set; }
    }
}