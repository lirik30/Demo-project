using BLL.Interfaces.Entities;
using DAL.Interfaces.DTO;

namespace BLL.Mappers
{
    public static class BllMappers
    {
        //User________
        public static DalUser ToDalUser(this UserEntity user)
        {
            return new DalUser
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Image = user.Image,
                BlogId = user.BlogId,
                RoleId = user.RoleId//
            };
        }

        public static UserEntity ToBllUser(this DalUser user)
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
                BlogId = user.BlogId,
                RoleId = user.RoleId
            };
        }

        //Role____________
        public static DalRole ToDalRole(this RoleEntity role)
        {
            return new DalRole
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static RoleEntity ToBllRole(this DalRole role)
        {
            return new RoleEntity
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        //Blog____________
        public static DalBlog ToDalBlog(this BlogEntity blog)
        {
            return new DalBlog
            {
                Id = blog.Id,
                BlogName = blog.BlogName,
                Description = blog.Description,
                CreateTime = blog.CreateTime,
                UserId = blog.UserId
            };
        }

        public static BlogEntity ToBllBlog(this DalBlog blog)
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

        //Post________
        public static DalPost ToDalPost(this PostEntity post)
        {
            return new DalPost
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

        public static PostEntity ToBllPost(this DalPost post)
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
        public static DalComment ToDalComment(this CommentEntity comment)
        {
            return new DalComment
            {
                Id = comment.Id,
                Content = comment.Content,
                CreateTime = comment.CreateTime,
                PostId = comment.PostId,
                UserId = comment.UserId
            };
        }

        public static CommentEntity ToBllComment(this DalComment comment)
        {
            return new CommentEntity
            {
                Id = comment.Id,
                Content = comment.Content,
                CreateTime = comment.CreateTime,
                PostId = comment.PostId,
                UserId = comment.UserId
            };
        }

    }
}
