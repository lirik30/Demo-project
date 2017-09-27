using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using BLL.Mappers;
using DAL.Interfaces.Repository;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository) => _repository = repository;

        public IEnumerable<RoleEntity> GetAllRoleEntities()
        {
            return _repository.GetAll().Select(role => role.ToBllRole());
        }

        public RoleEntity GetRoleEntity(int id)
        {
            return _repository.GetById(id)?.ToBllRole();
        }

        public RoleEntity GetRoleByName(string name)
        {
            return _repository.GetByName(name)?.ToBllRole();
        }
    }
}
