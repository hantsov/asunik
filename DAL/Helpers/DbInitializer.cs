using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Identity;
using Microsoft.AspNet.Identity;

namespace DAL.Helpers
{
    class DbInitializer : DropCreateDatabaseAlways<DatabaseContext>
    {
        //protected override void Seed(DatabaseContext context)
        //{
        //    //    context.Users.Add(new User
        //    //    {
        //    //        Firstname = "Pets",
        //    //        Lastname = "Rets",
        //    //        IdCode = "123",
        //    //        Birthdate = DateTime.Now,
        //    //        CodeIssuer = "ESTONIA"
        //    //    });
        //    //    context.Users.Add(new User
        //    //    {
        //    //        Firstname = "Anti",
        //    //        Lastname = "Friis",
        //    //        IdCode = "345",
        //    //        Birthdate = DateTime.Now,
        //    //        CodeIssuer = "FINLAND"
        //    //    });
        //    //    context.SaveChanges();
        //}
        protected override void Seed(DatabaseContext context)
        {
            var pwdHasher = new PasswordHasher();

            // Roles
            context.Roles.Add(new Role()
            {
                Name = "Admin"
            });

            context.SaveChanges();

            context.Roles.Add(new Role()
            {
                Name = "User"
            });

            context.SaveChanges();

            // Users
            User User;
            context.Users.Add(User = new User()
            {
                UserName = "1@eesti.ee",
                Email = "1@eesti.ee",
                FirstName = "Super",
                LastName = "User",
                PasswordHash = pwdHasher.HashPassword("a"),
                SecurityStamp = Guid.NewGuid().ToString()
            });

            context.SaveChanges();

            User User2;
            context.Users.Add(User2 = new User()
            {
                UserName = "2@eesti.ee",
                Email = "2@eesti.ee",
                FirstName = "Simple",
                LastName = "User",
                PasswordHash = pwdHasher.HashPassword("b"),
                SecurityStamp = Guid.NewGuid().ToString()
            });

            context.SaveChanges();

            // Users in Roles
            UserRole UserRole;
            context.UserRoles.Add(UserRole = new UserRole()
            {
                User = context.Users.FirstOrDefault(a => a.UserName == "1@eesti.ee"),
                Role = context.Roles.FirstOrDefault(a => a.Name == "Admin")
            });
            context.SaveChanges();

            UserRole UserRole2;
            context.UserRoles.Add(UserRole2 = new UserRole()
            {
                User = context.Users.FirstOrDefault(a => a.UserName == "2@eesti.ee"),
                Role = context.Roles.FirstOrDefault(a => a.Name == "User")
            });
            context.SaveChanges();

            base.Seed(context);
        }

    }
}
