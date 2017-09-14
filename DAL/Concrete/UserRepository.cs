using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repository;
using EFModel;

namespace DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _context;

        public UserRepository(DbContext context) => _context = context;

        public IEnumerable<DalUser> GetAll()
        {
            return _context.Set<User>().Select(user => new DalUser
            {
                Id = user.UserId,
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BlogId = user.Blog == null ? null : (int?)user.Blog.BlogId,
                Image = user.Image//...
            });
        }

        public DalUser GetById(int key)
        {
            var user = _context.Set<User>().SingleOrDefault(u => u.UserId == key);
            return user == null 
                ? null 
                : new DalUser
                {
                    Id = user.UserId,
                    Login = user.Login,
                    Password = user.Password,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BlogId = user.Blog?.BlogId,
                    Image = user.Image
                };
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> predicate)//!!!
        {
            var userParameter = Expression.Parameter(typeof(User), "user");
            var boolParameter = Expression.Parameter(typeof(bool), "b");
            var newPredicate = Expression.Lambda<Func<User, bool>>(
                body: predicate.Body, 
                parameters: new[]{userParameter, boolParameter});

            var user = _context.Set<User>().SingleOrDefault(newPredicate);
            return user == null 
                ? null 
                : new DalUser
                {
                    Id = user.UserId,
                    Login = user.Login,
                    Password = user.Password,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BlogId = user.Blog?.BlogId,
                    Image = user.Image
                };
        }

        public void Create(DalUser dUser)
        {
            Debug.WriteLine("________________" + dUser.Login);

            var user = new User
            {
                UserId = dUser.Id,
                Login = dUser.Login,
                Password = dUser.Password,
                Email = dUser.Email,
                FirstName = dUser.FirstName,
                LastName = dUser.LastName,
                Image = dUser.Image,
                Blog = _context.Set<Blog>().SingleOrDefault(b => b.BlogId == dUser.BlogId)//..It's good? it's redundant!
            };


            //Debug.WriteLine("________Id" + user.UserId);
            //Debug.WriteLine("_____Login" + user.Login);
            //Debug.WriteLine("__Password" + user.Password);
            //Debug.WriteLine("_____Email" + user.Email);
            //Debug.WriteLine("First name" + user.FirstName);
            //Debug.WriteLine("_Last name" + user.LastName);
            //Debug.WriteLine("_Blog name" + (user.Blog?.BlogName ?? "NoBlog"));

            _context.Set<User>().Add(user);
        }

        public void Update(DalUser dUser)
        {
            var user = _context.Set<User>().Single(u => u.UserId == dUser.Id);
            user.Login = dUser.Login;
            user.Password = dUser.Password;
            user.Email = dUser.Email;
            user.FirstName = dUser.FirstName;
            user.LastName = dUser.LastName;
            user.Image = dUser.Image;
            //user.Blog = _context.Set<Blog>().SingleOrDefault(blog => blog.BlogId == dUser.BlogId);

            _context.Entry(user).State = EntityState.Modified;
        }

        public void Delete(DalUser dUser)
        {
            var user = _context.Set<User>().Single(u => u.UserId == dUser.Id); //try?
            _context.Set<User>().Remove(user);
        }
    }
}
