using System;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "You need fill the text of the article"), DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public string Tag { get; set; }
        [Display(Name="Time of creation")]
        public DateTime CreateTime { get; set; }
        [Display(Name = "Time of last updating")]
        public DateTime UpdateTime { get; set; }

        public int BlogId { get; set; }
    }
}