using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Services
{
    public interface IPostService
    {
        IEnumerable<PostEntity> GetAllPostEntities();
        PostEntity GetPostEntity(int id);
        PostEntity GetByPredicate(Expression<Func<PostEntity, bool>> predicate);
        void CreatePost(PostEntity post);
        void UpdatePost(PostEntity post);
        void DeletePost(PostEntity post);
    }
}
