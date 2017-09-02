using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interfaces.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcPLMappers
    {
        //User______
        public static UserViewModel ToMvcUser(this UserEntity user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Image = user.Image,
                BlogId = user.BlogId
            };
        }

        public static UserEntity ToBllUser(this UserViewModel user)
        {
            return new UserEntity
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Image = user.Image,
                BlogId = user.BlogId
            };
        }

        //Blog______
        public static BlogViewModel ToMvcBlog(this BlogEntity blog)
        {
            return new BlogViewModel
            {
                Id = blog.Id,
                BlogName = blog.BlogName,
                Description = blog.Description,
                CreateTime = blog.CreateTime,
                UserId = blog.UserId
            };
        }

        public static BlogEntity ToBllBlog(this BlogViewModel blog)
        {
            return new BlogEntity
            {
                Id = blog.Id,
                BlogName = blog.BlogName,
                Description = blog.Description,
                CreateTime = blog.CreateTime,
                UserId = blog.UserId
            };
        }

        //Post______
        public static PostViewModel ToMvcPost(this PostEntity post)
        {
            return new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Tag = post.Tag,
                CreateTime = post.CreateTime,
                UpdateTime = post.UpdateTime,
                BlogId = post.BlogId
            };
        }

        public static PostEntity ToBllPost(this PostViewModel post)
        {
            return new PostEntity
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Tag = post.Tag,
                CreateTime = post.CreateTime,
                UpdateTime = post.UpdateTime,
                BlogId = post.BlogId
            };
        }

        //Comment______
        public static CommentViewModel ToMvcComment(this CommentEntity comment)
        {
            return new CommentViewModel
            {
                Id = comment.Id,
                Content = comment.Content,
                CreateTime = comment.CreateTime,
                UserId = comment.UserId,
                PostId = comment.PostId
            };
        }

        public static CommentEntity ToBllComment(this CommentViewModel comment)
        {
            return new CommentEntity
            {
                Id = comment.Id,
                Content = comment.Content,
                CreateTime = comment.CreateTime,
                UserId = comment.UserId,
                PostId = comment.PostId
            };
        }
    }
}