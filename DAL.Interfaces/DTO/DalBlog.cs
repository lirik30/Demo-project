using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.DTO
{
    public class DalBlog : IEntity
    {
        public int Id { get; set; }
        public string BlogName { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }

        public int UserId { get; set; }
    }
}
