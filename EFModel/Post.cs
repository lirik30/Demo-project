using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFModel
{
    public class Post
    {
        public Post() => Comments = new List<Comment>();

        public int PostId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string Tag { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public int BlogId { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }  
    }
}
