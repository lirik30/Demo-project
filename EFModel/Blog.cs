using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFModel
{
    public class Blog
    {
        public Blog() => Posts = new List<Post>();

        [ForeignKey("User")]
        public int BlogId { get; set; }
        public string BlogName { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        
        public virtual User User { get; set; }
        public virtual ICollection<Post> Posts { get; set; } 
    }
}
