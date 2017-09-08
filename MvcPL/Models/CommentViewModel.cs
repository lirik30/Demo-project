using System;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        [Display(Name = "Time of creation")]
        public DateTime CreateTime { get; set; }

        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}