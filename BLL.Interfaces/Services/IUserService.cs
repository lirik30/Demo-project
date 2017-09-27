using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Services
{
    public interface IUserService
    {
        int? GetIdByLogin(string login);
        string GetLoginById(int id);
        IEnumerable<UserEntity> GetAllUserEntities();
        UserEntity GetUserEntity(int id);
        UserEntity GetUserByLogin(string login);
        UserEntity GetByPredicate(Expression<Func<UserEntity, bool>> predicate);
        void CreateUser(UserEntity user);
        void UpdateUser(UserEntity user);
        void DeleteUser(UserEntity user);
    }
}
