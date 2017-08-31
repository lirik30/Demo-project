using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public class BlogEntity
    {
        public int Id { get; set; }
        public string BlogName { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }

        public int UserId { get; set; }
    }
}
