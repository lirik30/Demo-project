using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModel;

namespace ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloghostContext())
            {
                Console.WriteLine("----START----");

                var user1 = new User {FirstName = "Ivan", LastName = "Ivanov", Bio = "Student"};
                var user2 = new User {FirstName = "Petr", LastName = "Petrov", Bio = "Student"};
                var user3 = new User {FirstName = "Andrey", LastName = "Andreev", Bio = "Professor"};

                db.Users.Add(user1);
                db.Users.Add(user2);
                db.Users.Add(user3);
                db.SaveChanges();

                foreach (var person in db.Users)
                {
                    Console.WriteLine(person.Bio);
                }

                Console.ReadKey();
            }
        }
    }
}
