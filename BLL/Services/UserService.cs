using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using BLL.Mappers;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repository;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IUnitOfWork _unit;

        public UserService(IUserRepository repository, IUnitOfWork unit)
        {
            _repository = repository;
            _unit = unit;
        }

        public IEnumerable<UserEntity> GetAllUserEntities()
        {
            return _repository.GetAll().Select(user => user.ToBllUser());
        }

        public UserEntity GetUserEntity(int id)
        {
            return _repository.GetById(id).ToBllUser();
        }

        public UserEntity GetByPredicate(Expression<Func<UserEntity, bool>> predicate)//!!!!!!!!!!
        {
            var userParameter = Expression.Parameter(typeof(DalUser), "user");
            var boolParameter = Expression.Parameter(typeof(bool), "b");
            var newPredicate = Expression.Lambda<Func<DalUser, bool>>(
                body: predicate.Body,
                parameters: new[] { userParameter, boolParameter });

            return _repository.GetByPredicate(newPredicate).ToBllUser();
        }

        public void CreateUser(UserEntity user)
        {
            _repository.Create(user.ToDalUser());
            _unit.Commit();
        }

        public void UpdateUser(UserEntity user)
        {
            _repository.Update(user.ToDalUser());
            _unit.Commit();
        }

        public void DeleteUser(UserEntity user)
        {
            _repository.Delete(user.ToDalUser());
            _unit.Commit();
        }
    }
}
