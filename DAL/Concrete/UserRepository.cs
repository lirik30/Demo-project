﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> predicate)
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
            var user = new User
            {
                UserId = dUser.Id,
                Login = dUser.Login,
                Password = dUser.Password,
                Email = dUser.Email,
                FirstName = dUser.FirstName,
                LastName = dUser.LastName,
                Image = dUser.Image,
                //Blog = ..TODO: think about how to deal with blog/blogId
            };
        }

        public void Update(DalUser dUser)
        {
            throw new NotImplementedException();
        }

        public void Delete(DalUser dUser)
        {
            throw new NotImplementedException();
        }
    }
}
