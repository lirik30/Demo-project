using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Services
{
    public interface IRoleService
    {
        IEnumerable<RoleEntity> GetAllRoleEntities();
        RoleEntity GetRoleEntity(int id);
        RoleEntity GetRoleByName(string name);
    }
}
