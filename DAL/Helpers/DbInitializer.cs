﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Album;
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
            seedAlbums(context);
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

            context.Roles.Add(new Role()
            {
                Name = "User"
            });

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
                User = context.Users.FirstOrDefault(a => a.UserName == "hardi@test.ee"),
                Role = context.Roles.FirstOrDefault(a => a.Name == "User")
            });

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
                Type = "NEWS",
                Content = "Shred guitar or shredding is a virtuoso lead guitar solo playing style for the guitar, based on various fast playing techniques." +
                          "Speed Building, Legato, Tapping, [and] Sweep Picking techniques shredders need to know—sweep picking, tapping, legato playing, whammy bar tricks, speed riffing, [and] thrash chording." +
                          " Shred guitarists use two- or three-octave scales, triads, or modes, played ascending and descending at a fast tempo."
            });

            context.Events.Add(new Event()
            {
                Author = context.Users.FirstOrDefault(a => a.UserName == "juhan@test.ee"),
                Heading = "New teacher joins with us!",
                Type = "NEWS",
                Content = "Please give a warm welcome to Mr. Anti Friis who will be taking over guitar courses."
            });

            context.Events.Add(new Event()
            {
                Author = context.Users.FirstOrDefault(a => a.UserName == "juhan@test.ee"),
                Heading = "The new website!",
                Type = "NEWS",
                Content = "Hope you all like our new and awesome website. Please register a user and try it out."
            });

            context.Events.Add(new Event()
            {
                Author = context.Users.FirstOrDefault(a => a.UserName == "juhan@test.ee"),
                Heading = "Friday night jam!",
                Type = "PARTICIPATORY",
                Content = "We are going to jam, join in!"
            });

            context.SaveChanges();

            // Members
            context.EventMembers.Add(new EventMember()
            {
                Event = context.Events.FirstOrDefault(c => c.Heading == "Friday night jam!"),
                Member = context.Users.FirstOrDefault(u => u.UserName == "hardi@test.ee"),
            });

            context.SaveChanges();
        }

        private void seedCourses(DatabaseContext context)
        {
            context.Courses.Add(new Course()
            {
                Heading = "Shred Guitar",
                Description = "Shred guitar or shredding is a virtuoso lead guitar solo playing style for the guitar, based on various fast playing techniques." +
                          "Speed Building, Legato, Tapping, [and] Sweep Picking techniques shredders need to know—sweep picking, tapping, legato playing, whammy bar tricks, speed riffing, [and] thrash chording." +
                          " Shred guitarists use two- or three-octave scales, triads, or modes, played ascending and descending at a fast tempo.",
                Level = "Mediocre",
                ImgLoc = "../../appContent/images/shred.jpg"
            });

            context.Courses.Add(new Course()
            {
                Heading = "English B2 -> C1",
                Description = "English level C1 is the fifth level of English in the Common European Framework of Reference (CEFR)," +
                              " a definition of different language levels written by the Council of Europe. In everyday speech, this level might be called “advanced”," +
                              " and that is the official level descriptor for this level as well, also used by EFSET." +
                              " At this level, students can function independently and with a great deal of precision on a wide variety of subjects and in almost any setting without any prior preparation.",
                Level = "Advanced",
                ImgLoc = "../../appContent/images/testcourse.jpg"
            });

            context.SaveChanges();

            context.CourseMembers.Add(new CourseMember()
            {
                Course = context.Courses.FirstOrDefault(c => c.Heading == "Shred Guitar"),
                User = context.Users.FirstOrDefault(u => u.UserName == "hardi@test.ee"),
                MemberRole = "STUDENT"
            });

            context.CourseMembers.Add(new CourseMember()
            {
                Course = context.Courses.FirstOrDefault(c => c.Heading == "Shred Guitar"),
                User = context.Users.FirstOrDefault(u => u.UserName == "juhan@test.ee"),
                MemberRole = "STUDENT"
            });

            context.SaveChanges();
        }

        private void seedAlbums(DatabaseContext context)
        {
            var pathBase = "../../appContent/images/gallery/";

            context.Albums.Add(new Album()
            {
                Heading = "Spring Fair 2017",
                SortOrder = 1,
                ThumbnailPath = pathBase + "springfair2017/thumbnail.jpg"
            });

            context.Albums.Add(new Album()
            {
                Heading = "Shred Guitar Demo 2017",
                SortOrder = 2,
                ThumbnailPath = pathBase + "shred2017/thumbnail.jpg"
            });

            context.Albums.Add(new Album()
            {
                Heading = "Spring 2017 Exam Period",
                SortOrder = 3,
                ThumbnailPath = pathBase + "exams2017/thumbnail.jpg"
            });

            context.SaveChanges();

            context.AlbumPhotos.Add(new AlbumPhoto()
            {
                FilePath = pathBase + "shred2017/shred0.jpg",
                SortOrder = 1,
                Photo = context.Albums.FirstOrDefault(a => a.Heading == "Shred Guitar Demo 2017")
            });

            context.AlbumPhotos.Add(new AlbumPhoto()
            {
                FilePath = pathBase + "shred2017/shred1.jpg",
                SortOrder = 1,
                Photo = context.Albums.FirstOrDefault(a => a.Heading == "Shred Guitar Demo 2017")
            });

            context.AlbumPhotos.Add(new AlbumPhoto()
            {
                FilePath = pathBase + "shred2017/shred2.jpg",
                SortOrder = 1,
                Photo = context.Albums.FirstOrDefault(a => a.Heading == "Shred Guitar Demo 2017")
            });


            context.AlbumPhotos.Add(new AlbumPhoto()
            {
                FilePath = pathBase + "springfair2017/springfair0.jpg",
                SortOrder = 1,
                Photo = context.Albums.FirstOrDefault(a => a.Heading == "Spring Fair 2017")
            });

            context.AlbumPhotos.Add(new AlbumPhoto()
            {
                FilePath = pathBase + "springfair2017/springfair1.jpg",
                SortOrder = 1,
                Photo = context.Albums.FirstOrDefault(a => a.Heading == "Spring Fair 2017")
            });

            context.AlbumPhotos.Add(new AlbumPhoto()
            {
                FilePath = pathBase + "springfair2017/springfair2.jpg",
                SortOrder = 1,
                Photo = context.Albums.FirstOrDefault(a => a.Heading == "Spring Fair 2017")
            });


            context.AlbumPhotos.Add(new AlbumPhoto()
            {
                FilePath = pathBase + "exams2017/exams0.jpg",
                SortOrder = 1,
                Photo = context.Albums.FirstOrDefault(a => a.Heading == "Spring 2017 Exam Period")
            });

            context.AlbumPhotos.Add(new AlbumPhoto()
            {
                FilePath = pathBase + "exams2017/exams1.jpg",
                SortOrder = 1,
                Photo = context.Albums.FirstOrDefault(a => a.Heading == "Spring 2017 Exam Period")
            });

            context.AlbumPhotos.Add(new AlbumPhoto()
            {
                FilePath = pathBase + "exams2017/exams2.jpg",
                SortOrder = 1,
                Photo = context.Albums.FirstOrDefault(a => a.Heading == "Spring 2017 Exam Period")
            });

            context.SaveChanges();
        }

    }
}
