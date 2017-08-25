using System;
using System.ComponentModel.DataAnnotations;

namespace EFModel
{
    public class Comment
    {
        public int CommentId { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        
        public int UserId { get; set; }
        public int PostId { get; set; }

        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
    }
}
