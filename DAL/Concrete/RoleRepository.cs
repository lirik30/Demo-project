using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repository;
using EFModel;

namespace DAL.Concrete
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DbContext _context;

        public RoleRepository(DbContext context) => _context = context;

        public IEnumerable<DalRole> GetAll()
        {
            return _context.Set<Role>().Select(r => new DalRole
            {
                Id = r.Id,
                Name = r.Name
            });
        }

        public DalRole GetById(int key)
        {
            var role = _context.Set<Role>().SingleOrDefault(r => r.Id == key);
            return role == null
                ? null
                : new DalRole
                {
                    Id = role.Id,
                    Name = role.Name
                };
        }

        public DalRole GetByName(string name)
        {
            var role = _context.Set<Role>().FirstOrDefault(r => r.Name == name);
            return role == null
                ? null
                : new DalRole
                {
                    Id = role.Id,
                    Name = role.Name
                };
        }

        #region NotImplemented
        public DalRole GetByPredicate(Expression<Func<DalRole, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Create(DalRole entity)
        {
            throw new NotImplementedException();
        }

        public void Update(DalRole entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(DalRole entity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
