using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Services
{
    public interface ICommentService
    {
        IEnumerable<CommentEntity> GetAllPostEntities();
        CommentEntity GetPostEntity(int id);
        void CreateComment(CommentEntity comment);
        void UpdateComment(CommentEntity comment);
        void DeleteComment(CommentEntity comment);
    }
}
