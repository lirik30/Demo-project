using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Services
{
    public interface IUserService
    {
        IEnumerable<UserEntity> GetAllUserEntities();
        UserEntity GetUserEntity(int id);
        UserEntity GetByPredicate(Expression<Func<UserEntity, bool>> predicate);
        void CreateUser(UserEntity user);
        void UpdateUser(UserEntity user);
        void DeleteUser(UserEntity user);
    }
}
