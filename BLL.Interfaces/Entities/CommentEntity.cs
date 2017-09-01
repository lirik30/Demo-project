using System;

namespace BLL.Interfaces.Entities
{
    public class CommentEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }

        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
