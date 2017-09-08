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
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repository;
        private readonly IUnitOfWork _unit;

        public BlogService(IBlogRepository repository, IUnitOfWork unit)
        {
            _repository = repository;
            _unit = unit;
        }

        public IEnumerable<BlogEntity> GetAllBlogEntities()
        {
            return _repository.GetAll().Select(blog => blog.ToBllBlog());
        }

        public BlogEntity GetBlogEntity(int id)
        {
            var blog = _repository.GetById(id);
            return blog?.ToBllBlog();
        }

        public BlogEntity GetByPredicate(Expression<Func<BlogEntity, bool>> predicate)
        {
            var blogParameter = Expression.Parameter(typeof(DalBlog), "blog");//.........!!!!!!!!
            var boolParameter = Expression.Parameter(typeof(bool), "b");
            var newPredicate = Expression.Lambda<Func<DalBlog, bool>>(
                body: predicate.Body,
                parameters: new[] { blogParameter, boolParameter });

            var blog = _repository.GetByPredicate(newPredicate);
            return blog?.ToBllBlog();
        }

        public void CreateBlog(BlogEntity blog)
        {
            _repository.Create(blog.ToDalBlog());
            _unit.Commit();
        }

        public void UpdateBlog(BlogEntity blog)
        {
            _repository.Update(blog.ToDalBlog());
            _unit.Commit();
        }

        public void DeleteBlog(BlogEntity blog)
        {
            _repository.Delete(blog.ToDalBlog());
            _unit.Commit();
        }
    }
}
