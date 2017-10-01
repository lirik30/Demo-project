using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using BLL.Mappers;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repository;

namespace BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IUnitOfWork _unit;

        public CommentService(ICommentRepository repository, IUnitOfWork unit)
        {
            _repository = repository;
            _unit = unit;
        }


        public IEnumerable<CommentEntity> GetAllPostEntities()
        {
            return _repository.GetAll().Select(comment => comment.ToBllComment());
        }

        public CommentEntity GetPostEntity(int id)
        {
            var comment = _repository.GetById(id);
            return comment?.ToBllComment();
        }

        public void CreateComment(CommentEntity comment)
        {
            _repository.Create(comment.ToDalComment());
            _unit.Commit();
        }

        public void UpdateComment(CommentEntity comment)
        {
            _repository.Update(comment.ToDalComment());
            _unit.Commit();
        }

        public void DeleteComment(CommentEntity comment)
        {
            _repository.Delete(comment.ToDalComment());
            _unit.Commit();
        }
    }
}
