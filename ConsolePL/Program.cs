using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using EFModel;

namespace ConsolePL
{
    class Program
    {
        static void EFModelTest()
        {
            using (var db = new BloghostContext())
            {
                Console.WriteLine("----START----");

                var user1 = new User { FirstName = "Ivan", LastName = "Ivanov", Bio = "Student" };
                var user2 = new User { FirstName = "Petr", LastName = "Petrov", Bio = "Student" };
                var user3 = new User { FirstName = "Andrey", LastName = "Andreev", Bio = "Professor" };

                var blog1 = new Blog { BlogName = "User1 Blog", CreateTime = DateTime.Now, UpdateTime = DateTime.Now };
                var blog2 = new Blog { BlogName = "User2 Blog", CreateTime = DateTime.Now, UpdateTime = DateTime.Now };
                var blog3 = new Blog { BlogName = "User3 Blog", CreateTime = DateTime.Now, UpdateTime = DateTime.Now };

                //db.Users.Add(user1);
                //db.Users.Add(user2);
                //db.Users.Add(user3);
                //db.SaveChanges();
                //var u1 = db.Users.Single(user => user.FirstName == user1.FirstName);
                //u1.Blog = blog1;

                //var u2 = db.Users.Single(user => user.FirstName == user2.FirstName);
                //u2.Blog = blog2;

                //var u3 = db.Users.Single(user => user.FirstName == user3.FirstName);
                //u3.Blog = blog3;
                //db.SaveChanges();

                var post1 = new Post
                {
                    Blog = blog1,
                    Title = "EF is awesome?",
                    Content = "I don't know...",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    Tag = "IT"
                };

                var post2 = new Post
                {
                    Blog = blog1,
                    Title = "EF is awesome!",
                    Content = "It's true",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    Tag = "IT"
                };

                //var updateBlog = db.Blogs.Single(blog => blog.BlogName == post1.Blog.BlogName);
                //updateBlog.Posts.Add(post1);
                //updateBlog.Posts.Add(post2);
                //db.SaveChanges();


                var comment1 = new Comment { Content = "Awesome!", CreateTime = DateTime.Now, Post = post1, User = user2 };
                var comment2 = new Comment { Content = "Double awesome!", CreateTime = DateTime.Now, Post = post1, User = user3 };

                //db.Comments.Add(comment2);
                //db.Comments.Add(comment1);

                //var updateUser = db.Users.Single(user => user.FirstName == "Ivan");
                //Console.WriteLine(updateUser.Blog.BlogName);
                //var updatePost = db.Posts.Single(post => post.Content == "I don't know...");
                //foreach (var comm in updatePost.Comments)
                //{
                //    Console.WriteLine(comm.Content);
                //}
                //Console.ReadKey();
                //db.Posts.Remove(updatePost);//comments aren't deleted:( already deleted 
                //db.Users.Remove(updateUser);
                //updatePost.Comments.Add(comment1);
                //updatePost.Comments.Add(comment2);
                //Console.WriteLine(updatePost.Blog.BlogName);
                //db.SaveChanges();


                foreach (var person in db.Users)
                {
                    Console.WriteLine(person.Blog.BlogName);
                    foreach (var post in person.Blog.Posts)
                    {
                        Console.WriteLine($"\t{post.Title}");
                        foreach (var comment in post.Comments)
                        {
                            Console.WriteLine($"\t\t{comment.User.FirstName} - {comment.Content}");
                        }
                    }
                }

                Console.ReadKey();
            }
        }

        static void Main(string[] args)
        {
            EFModelTest();
        }
    }
}
