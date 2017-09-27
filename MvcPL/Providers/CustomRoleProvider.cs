using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BLL.Interfaces.Services;

namespace MvcPL.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public IUserService UserService => (IUserService)
            System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));

        public IRoleService RoleService => (IRoleService)
            System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));

        public override bool IsUserInRole(string username, string roleName)
        {
            var user = UserService.GetUserByLogin(username);
            if (user == null) return false;

            var role = RoleService.GetRoleEntity(user.RoleId);
            return role != null && role.Name == roleName;
        }

        //почему в примере проекта работали через контекст, а не через сервисы?
        public override string[] GetRolesForUser(string username)
        {
            var roles = new string[] {};

            var user = UserService.GetUserByLogin(username);
            if (user == null) return null;

            var role = RoleService.GetRoleEntity(user.RoleId);
            if(role != null)
                roles = new []{role.Name};

            return roles;
        }

        #region NotImplemented
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }

        #endregion
    }
}