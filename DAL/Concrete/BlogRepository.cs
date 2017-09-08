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
    public class BlogRepository : IBlogRepository
    {
        private readonly DbContext _context;

        public BlogRepository(DbContext context) => _context = context;

        public IEnumerable<DalBlog> GetAll()
        {
            return _context.Set<Blog>().Select(blog => new DalBlog
            {
                Id = blog.BlogId,
                BlogName = blog.BlogName,
                Description = blog.Description,
                CreateTime = blog.CreateTime,
                UserId = blog.User.UserId
            });
        }

        public DalBlog GetById(int key)
        {
            var blog = _context.Set<Blog>().SingleOrDefault(b => b.BlogId == key);
            return blog == null
                ? null
                : new DalBlog
                {
                    Id = blog.BlogId,
                    BlogName = blog.BlogName,
                    Description = blog.Description,
                    CreateTime = blog.CreateTime,
                    UserId = blog.User.UserId
                };
        }

        public DalBlog GetByPredicate(Expression<Func<DalBlog, bool>> predicate)
        {
            var blogParameter = Expression.Parameter(typeof(Blog), "blog");
            var boolParameter = Expression.Parameter(typeof(bool), "b");
            var newPredicate = Expression.Lambda<Func<Blog, bool>>(
                body: predicate.Body,
                parameters: new[] { blogParameter, boolParameter });

            var blog = _context.Set<Blog>().SingleOrDefault(newPredicate);
            return blog == null
                ? null
                : new DalBlog
                {
                    Id = blog.BlogId,
                    BlogName = blog.BlogName,
                    Description = blog.Description,
                    CreateTime = blog.CreateTime,
                    UserId = blog.User.UserId
                };
        }

        public void Create(DalBlog dBlog)
        {
            var blog = new Blog
            {
                BlogId = dBlog.Id,
                BlogName = dBlog.BlogName,
                Description = dBlog.Description,
                CreateTime = dBlog.CreateTime,
                User = _context.Set<User>().SingleOrDefault(u => u.UserId == dBlog.UserId)
            };
            _context.Set<Blog>().Add(blog);
        }

        public void Update(DalBlog dBlog)
        {
            var blog = _context.Set<Blog>().Single(b => b.BlogId == dBlog.Id);
            blog.BlogName = dBlog.BlogName;
            blog.Description = dBlog.Description; //Only blogname and description will be updated
            _context.Entry(blog).State = EntityState.Modified;
        }

        public void Delete(DalBlog dBlog)
        {
            var blog = _context.Set<Blog>().Single(b => b.BlogId == dBlog.Id);
            _context.Set<Blog>().Remove(blog);
        }
    }
}
