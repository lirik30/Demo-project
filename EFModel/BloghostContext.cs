using System.Data.Entity;

namespace EFModel
{
    public class BloghostContext : DbContext
    {
        public BloghostContext() : base("name=BloghostContext") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
