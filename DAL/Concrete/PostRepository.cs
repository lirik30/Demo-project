using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repository;
using EFModel;

//TODO: mappers?
namespace DAL.Concrete
{
    public class PostRepository : IPostRepository
    {
        private readonly DbContext _context;

        public PostRepository(DbContext context) => _context = context;

        public IEnumerable<DalPost> GetAll()
        {
            return _context.Set<Post>().Select(post => new DalPost
            {
                Id = post.PostId,
                Title = post.Title,
                Content = post.Content,
                Tag = post.Tag,
                CreateTime = post.CreateTime,
                UpdateTime = post.UpdateTime,
                BlogId = post.BlogId
            });
        }

        public DalPost GetById(int key)
        {
            var post = _context.Set<Post>().SingleOrDefault(p => p.PostId == key);
            return post == null
                ? null
                : new DalPost
                {
                    Id = post.PostId,
                    Title = post.Title,
                    Content = post.Content,
                    Tag = post.Tag,
                    CreateTime = post.CreateTime,
                    UpdateTime = post.UpdateTime,
                    BlogId = post.BlogId
                };
        }

        public DalPost GetByPredicate(Expression<Func<DalPost, bool>> predicate)
        {
            var postParameter = Expression.Parameter(typeof(Post), "post");
            var boolParameter = Expression.Parameter(typeof(bool), "b");
            var newPredicate = Expression.Lambda<Func<Post, bool>>(
                body: predicate.Body,
                parameters: new[] { postParameter, boolParameter });

            var post = _context.Set<Post>().SingleOrDefault(newPredicate);
            return post == null
                ? null
                : new DalPost
                {
                    Id = post.PostId,
                    Title = post.Title,
                    Content = post.Content,
                    Tag = post.Tag,
                    CreateTime = post.CreateTime,
                    UpdateTime = post.UpdateTime,
                    BlogId = post.BlogId
                };
        }

        public void Create(DalPost dPost)
        {
            var post = new Post
            {
                PostId = dPost.Id,
                Title = dPost.Title,
                Content = dPost.Content,
                Tag = dPost.Tag,
                CreateTime = dPost.CreateTime,
                UpdateTime = dPost.UpdateTime,
                BlogId = dPost.BlogId,  //todo: can i set value for BlogId instead of set value for Blog? 
                //Blog = _context.Set<Blog>().SingleOrDefault(b => b.BlogId == dPost.BlogId)
            };
            _context.Set<Post>().Add(post);
        }

        public void Update(DalPost dPost)
        {
            var post = new Post
            {
                PostId = dPost.Id,
                Title = dPost.Title,
                Content = dPost.Content,
                Tag = dPost.Tag,
                CreateTime = dPost.CreateTime,
                UpdateTime = dPost.UpdateTime,
                BlogId = dPost.BlogId
                //Blog = _context.Set<Blog>().SingleOrDefault(b => b.BlogId == dPost.BlogId)
            };

            _context.Entry(post).State = EntityState.Modified;
        }

        public void Delete(DalPost dPost)
        {
            var post = new Post
            {
                PostId = dPost.Id,
                Title = dPost.Title,
                Content = dPost.Content,
                Tag = dPost.Tag,
                CreateTime = dPost.CreateTime,
                UpdateTime = dPost.UpdateTime,
                BlogId = dPost.BlogId
                //Blog = _context.Set<Blog>().SingleOrDefault(b => b.BlogId == dPost.BlogId)
            };

            _context.Set<Post>().Remove(post);
        }
    }
}
