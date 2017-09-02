using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Tag { get; set; }
        [Display(Name="Time of creation")]
        public DateTime CreateTime { get; set; }
        [Display(Name = "Time of last updating")]
        public DateTime UpdateTime { get; set; }

        public int BlogId { get; set; }
    }
}