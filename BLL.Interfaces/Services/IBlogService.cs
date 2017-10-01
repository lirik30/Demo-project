using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Services
{
    public interface IBlogService
    {
        IEnumerable<BlogEntity> GetAllBlogEntities();
        BlogEntity GetBlogEntity(int id);
        void CreateBlog(BlogEntity blog);
        void UpdateBlog(BlogEntity blog);
        void DeleteBlog(BlogEntity blog);
    }
}
