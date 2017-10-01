using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repository;
using EFModel;

namespace DAL.Concrete
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DbContext _context;

        public CommentRepository(DbContext context) => _context = context;

        public IEnumerable<DalComment> GetAll()
        {
            return _context.Set<Comment>().Select(comment => new DalComment
            {
                Id = comment.CommentId,
                Content = comment.Content,
                CreateTime = comment.CreateTime,
                PostId = comment.PostId,
                UserId = comment.UserId
            });
        }

        public DalComment GetById(int key)
        {
            var comment = _context.Set<Comment>().SingleOrDefault(c => c.CommentId == key);
            return comment == null
                ? null
                : new DalComment
                {
                    Id = comment.CommentId,
                    Content = comment.Content,
                    CreateTime = comment.CreateTime,
                    PostId = comment.PostId,
                    UserId = comment.UserId
                };
        }

        public void Create(DalComment dComment)
        {
            var comment = new Comment
            {
                CommentId = dComment.Id,
                Content = dComment.Content,
                CreateTime = dComment.CreateTime,
                PostId = dComment.PostId,
                UserId = dComment.UserId
            };
            _context.Set<Comment>().Add(comment);
        }

        public void Update(DalComment dComment)
        {
            var comment = new Comment
            {
                CommentId = dComment.Id,
                Content = dComment.Content,
                CreateTime = dComment.CreateTime,
                PostId = dComment.PostId,
                UserId = dComment.UserId
            };
            _context.Entry(comment).State = EntityState.Modified;
        }

        public void Delete(DalComment dComment)
        {
            var comment = new Comment
            {
                CommentId = dComment.Id,
                Content = dComment.Content,
                CreateTime = dComment.CreateTime,
                PostId = dComment.PostId,
                UserId = dComment.UserId
            };
            _context.Set<Comment>().Remove(comment);
        }
    }
}
