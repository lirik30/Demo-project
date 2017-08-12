using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFModel
{
    public class BloghostContext : DbContext
    {
        public BloghostContext() : base("BloghostDB") { }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Comment>()
        //        .HasRequired<Post>(c => c.Post)
        //        .WithMany().HasForeignKey(c => c.PostId)
        //        .WillCascadeOnDelete(true);

        //    modelBuilder.Entity<Comment>()
        //        .HasRequired<User>(c => c.User)
        //        .WithMany().HasForeignKey(c => c.UserId)
        //        .WillCascadeOnDelete(true);

        //    modelBuilder.Entity<Post>()
        //        .HasRequired<Blog>(p => p.Blog)
        //        .WithMany().HasForeignKey(p => p.BlogId)
        //        .WillCascadeOnDelete(true);

        //    modelBuilder.Entity<User>()
        //        .HasOptional(u => u.Blog)
        //        .WithRequired(b => b.User)
        //        .WillCascadeOnDelete(true);
        //}

        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
