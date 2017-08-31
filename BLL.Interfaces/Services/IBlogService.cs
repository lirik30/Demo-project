using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Services
{
    public interface IBlogService
    {
        IEnumerable<BlogEntity> GetAllBlogEntities();
        BlogEntity GetBlogEntity(int id);
        BlogEntity GetByPredicate(Expression<Func<BlogEntity, bool>> predicate);
        void CreateBlog(BlogEntity blog);
        void UpdateBlog(BlogEntity blog);
        void DeleteBlog(BlogEntity blog);
    }
}
