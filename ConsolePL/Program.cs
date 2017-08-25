using System;
using System.Data.Entity;
using System.Linq;
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

                var user1 = new User
                {
                    Login = "first",
                    Password = "blabla&%",
                    Email = "first@gmail.com",
                    FirstName = "First",
                    LastName = "Firstoff"
                };
                var user2 = new User
                {
                    Login = "second",
                    Password = "blabla&%",
                    Email = "second@gmail.com",
                    FirstName = "Second",
                    LastName = "Secondoff"
                };
                var user3 = new User
                {
                    Login = "third",
                    Password = "blabla&%",
                    Email = "third@gmail.com",
                    FirstName = "Third",
                    LastName = "Thirdoff"
                };

                var blog1 = new Blog
                {
                    BlogName = "First blog",
                    CreateTime = DateTime.Now,
                };
                var blog2 = new Blog
                {
                    BlogName = "Second blog",
                    CreateTime = DateTime.Now,
                };
                var blog3 = new Blog
                {
                    BlogName = "Third blog",
                    CreateTime = DateTime.Now,
                };

                user1.Blog = blog1;
                user2.Blog = blog2;
                user3.Blog = blog3;
                db.Users.Add(user1);
                db.Users.Add(user2);
                db.Users.Add(user3);
                db.SaveChanges();

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

                var post3 = new Post
                {
                    Blog = blog3,
                    Title = "Что-то очень замудрёное",
                    Content = "блабла",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    Tag = "tag"
                };

                db.Posts.Add(post1);
                db.SaveChanges();
                db.Posts.Add(post2);
                db.SaveChanges(); //не работает, если добавлять записи по одной
                //db.SaveChanges();
                foreach (var user in db.Users)
                {
                    Console.WriteLine(user.Login);
                    Console.WriteLine(user.Blog.BlogName);
                    foreach (var post in user.Blog.Posts)
                    {
                        Console.WriteLine("\t"+post.Title);
                    }
                }

                //var updateBlog3 = db.Blogs.FirstOrDefault(b => b.BlogName == blog3.BlogName);
                //updateBlog3.Posts.Add(post2);
                //updateBlog3.Posts.Add(post3);
                //db.SaveChanges();
                //blog1.Posts.Add(post1);
                //blog3.Posts.Add(post2);
                //blog3.Posts.Add(post3);
                //db.Entry(blog1).State = EntityState.Modified;
                //db.Entry(blog2).State = EntityState.Modified;
                //db.Entry(blog3).State = EntityState.Modified;
                //db.SaveChanges();

                //foreach (var blog in db.Blogs)
                //{
                //    Console.WriteLine(blog.BlogName);
                //    foreach (var post in db.Posts)
                //    {
                //        Console.WriteLine("\t" + post.Title);
                //    }
                //}

                //var updateBlog = db.Blogs.Single(blog => blog.BlogName == post1.Blog.BlogName);
                //updateBlog.Posts.Add(post1);
                //updateBlog.Posts.Add(post2);
                //db.SaveChanges();


                //var comment1 = new Comment { Content = "Awesome!", CreateTime = DateTime.Now, Post = post1, User = user2 };
                //var comment2 = new Comment { Content = "Double awesome!", CreateTime = DateTime.Now, Post = post1, User = user3 };

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


                //foreach (var person in db.Users)
                //{
                //    Console.WriteLine(person.Blog.BlogName);
                //    foreach (var post in person.Blog.Posts)
                //    {
                //        Console.WriteLine($"\t{post.Title}");
                //        foreach (var comment in post.Comments)
                //        {
                //            Console.WriteLine($"\t\t{comment.User.FirstName} - {comment.Content}");
                //        }
                //    }
                //}

                Console.ReadKey();
            }
        }

        static void Main(string[] args)
        {
            EFModelTest();
        }
    }
}
