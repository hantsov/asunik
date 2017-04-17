using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Course;
using Domain.Event;
using Domain.Identity;
using Microsoft.AspNet.Identity;

namespace DAL.Helpers
{
    class DbInitializer : DropCreateDatabaseAlways<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            seedIdentity(context);
            seedEvents(context);
            seedCourses(context);
            base.Seed(context);
        }


        private void seedIdentity(DatabaseContext context)
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
            context.Users.Add(new User()
            {
                UserName = "hardi@test.ee",
                Email = "hardi@test.ee",
                FirstName = "Hardi",
                LastName = "Antsov",
                PasswordHash = pwdHasher.HashPassword("test123"),
                SecurityStamp = Guid.NewGuid().ToString()
            });

            context.SaveChanges();

            context.Users.Add(new User()
            {
                UserName = "juhan@test.ee",
                Email = "juhan@test.ee",
                FirstName = "Juhan",
                LastName = "Trink",
                PasswordHash = pwdHasher.HashPassword("test123"),
                SecurityStamp = Guid.NewGuid().ToString()
            });

            context.SaveChanges();

            // Users in Roles
            context.UserRoles.Add(new UserRole()
            {
                User = context.Users.FirstOrDefault(a => a.UserName == "hardi@test.ee"),
                Role = context.Roles.FirstOrDefault(a => a.Name == "Admin")
            });
            context.SaveChanges();

            context.UserRoles.Add(new UserRole()
            {
                User = context.Users.FirstOrDefault(a => a.UserName == "juhan@test.ee"),
                Role = context.Roles.FirstOrDefault(a => a.Name == "User")
            });
            context.SaveChanges();

        }

        private void seedEvents(DatabaseContext context)
        {
            context.Events.Add(new Event()
            {
                Author = context.Users.FirstOrDefault(a => a.UserName == "hardi@test.ee"),
                Heading = "Now offering a course on shredding",
                Content = "Shred guitar or shredding is a virtuoso lead guitar solo playing style for the guitar, based on various fast playing techniques." +
                          "Speed Building, Legato, Tapping, [and] Sweep Picking techniques shredders need to know—sweep picking, tapping, legato playing, whammy bar tricks, speed riffing, [and] thrash chording." +
                          " Shred guitarists use two- or three-octave scales, triads, or modes, played ascending and descending at a fast tempo."
            });

            context.Events.Add(new Event()
            {
                Author = context.Users.FirstOrDefault(a => a.UserName == "juhan@test.ee"),
                Heading = "New teacher joins with us!",
                Content = "Please give a warm welcome to Mr. Anti Friis who will be taking over guitar courses."
            });

            context.Events.Add(new Event()
            {
                Author = context.Users.FirstOrDefault(a => a.UserName == "juhan@test.ee"),
                Heading = "The new website!",
                Content = "Hope you all like our new and awesome website. Please register a user and try it out."
            });

            context.SaveChanges();
        }

        private void seedCourses(DatabaseContext context)
        {
            context.Courses.Add(new Course()
            {
                Heading = "TestCourse",
                Description = "123Description",
                Level = "Mediocre",
                ImgLoc = "../../appContent/images/shred.jpg"
            });

            context.Courses.Add(new Course()
            {
                Heading = "TestCourse2",
                Description = "1234Description",
                Level = "Expert",
                ImgLoc = "../../appContent/images/testcourse.jpg"
            });

            context.SaveChanges();

            context.CourseMembers.Add(new CourseMember()
            {
                Course = context.Courses.FirstOrDefault(c => c.Heading == "TestCourse"),
                User = context.Users.FirstOrDefault(u => u.UserName == "hardi@test.ee"),
                MemberRole = "STUDENT"
            });

            context.CourseMembers.Add(new CourseMember()
            {
                Course = context.Courses.FirstOrDefault(c => c.Heading == "TestCourse"),
                User = context.Users.FirstOrDefault(u => u.UserName == "juhan@test.ee"),
                MemberRole = "STUDENT"
            });

            context.SaveChanges();
        }
    }
}
