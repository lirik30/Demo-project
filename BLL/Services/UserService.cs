﻿using System;
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

        public int? GetIdByLogin(string login)
        {
            return _repository.GetUserByLogin(login)?.Id;
        }

        public string GetLoginById(int id)
        {
            return _repository.GetById(id)?.Login;
        }

        public IEnumerable<UserEntity> GetAllUserEntities()
        {
            return _repository.GetAll().Select(user => user.ToBllUser());
        }

        public UserEntity GetUserEntity(int id)
        {
            var user = _repository.GetById(id);
            return user?.ToBllUser();
        }

        public UserEntity GetUserByLogin(string login)
        {
            var user = _repository.GetUserByLogin(login);
            return user?.ToBllUser();
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
