using System;

namespace DAL.Interfaces.DTO
{
    public class DalPost : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Tag { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public int BlogId { get; set; }
    }
}
