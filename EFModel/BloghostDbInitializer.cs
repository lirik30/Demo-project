using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFModel
{
    public class BloghostDbInitializer : CreateDatabaseIfNotExists<BloghostContext>
    {
        protected override void Seed(BloghostContext context)
        {
            base.Seed(context);
            context.Roles.Add(new Role{Id = 1, Name = "Administrator"});
            context.Roles.Add(new Role { Id = 2, Name = "User" });
            context.Users.Add(new User
            {
                UserId = 1,
                Login = "Admin",
                Password = "8ef78fe44aa1c12a08dba65c97bc5a3c",
                Email = "bloghostAdmin3008@gmail.ru",
                RoleId = 1
            });
        }
    }
}
