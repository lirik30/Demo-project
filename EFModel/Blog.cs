using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFModel
{
    public class Blog
    {
        public Blog() => Posts = new List<Post>();

        [ForeignKey("User")]
        public int BlogId { get; set; }
        [Required]
        public string BlogName { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        
        public virtual User User { get; set; }
        public virtual ICollection<Post> Posts { get; set; } 
    }
}
