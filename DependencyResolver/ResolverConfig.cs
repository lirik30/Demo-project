using System.Data.Entity;
using BLL.Interfaces.Services;
using BLL.Services;
using DAL.Concrete;
using DAL.Interfaces.Repository;
using EFModel;
using Ninject;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel) => Configure(kernel, true);

        public static void ConfigurateResolverConsole(this IKernel kernel) => Configure(kernel, false);

        private static void Configure(IKernel kernel, bool isWeb)
        {
            if (isWeb)
            {
                //And, maybe, nothing works because of this 
                //with insingletonscope it works, but...
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope(); //InRequestScope is unavailable! 
                kernel.Bind<DbContext>().To<BloghostContext>().InSingletonScope();
            }
            else
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
                kernel.Bind<DbContext>().To<BloghostContext>().InSingletonScope();
            }

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IUserRepository>().To<UserRepository>();

            kernel.Bind<IBlogService>().To<BlogService>();
            kernel.Bind<IBlogRepository>().To<BlogRepository>();

            kernel.Bind<IPostService>().To<PostService>();
            kernel.Bind<IPostRepository>().To<PostRepository>();

            kernel.Bind<ICommentService>().To<CommentService>();
            kernel.Bind<ICommentRepository>().To<CommentRepository>();
        }
    }
}
