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
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IUnitOfWork _unit;

        public PostService(IPostRepository repository, IUnitOfWork unit)
        {
            _repository = repository;
            _unit = unit;
        }

        public IEnumerable<PostEntity> GetAllPostEntities()
        {
            return _repository.GetAll().Select(post => post.ToBllPost());
        }

        public PostEntity GetPostEntity(int id)
        {
            var post = _repository.GetById(id);
            return post?.ToBllPost();
        }

        public PostEntity GetByPredicate(Expression<Func<PostEntity, bool>> predicate)
        {
            var postParameter = Expression.Parameter(typeof(DalPost), "post");
            var boolParameter = Expression.Parameter(typeof(bool), "b");
            var newPredicate = Expression.Lambda<Func<DalPost, bool>>(
                body: predicate.Body,
                parameters: new[] { postParameter, boolParameter });

            var post = _repository.GetByPredicate(newPredicate);
            return post?.ToBllPost();
        }

        public void CreatePost(PostEntity post)
        {
            _repository.Create(post.ToDalPost());
            _unit.Commit();
        }

        public void UpdatePost(PostEntity post)
        {
            _repository.Update(post.ToDalPost());
            _unit.Commit();
        }

        public void DeletePost(PostEntity post)
        {
            _repository.Delete(post.ToDalPost());
            _unit.Commit();
        }
    }
}
