using System;

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
