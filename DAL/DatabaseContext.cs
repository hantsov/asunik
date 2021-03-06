﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EFConfiguration;
using DAL.EFConfiguration.Identity;
using DAL.Helpers;
using Domain;
using Domain.Album;
using Domain.Course;
using Domain.Event;
using Domain.Identity;
using Interfaces;
using Interfaces.UOW;
using Ninject;
using NLog;

namespace DAL
{
    public class DatabaseContext : DbContext, IDbContext
    {
        private readonly NLog.ILogger _logger;
        private readonly string _instanceId = Guid.NewGuid().ToString();
        private readonly IUserNameResolver _userNameResolver;

        [Inject]
        public DatabaseContext(IUserNameResolver userNameResolver, ILogger logger)
            : base("name=DbConnectionString")
        {
            _logger = logger;
            _userNameResolver = userNameResolver;

            _logger.Debug("InstanceId: " + _instanceId);

            // use for azure
            //Database.SetInitializer<DatabaseContext>(null);
            Database.SetInitializer(new DbInitializer());


#if DEBUG
            this.Database.Log = s => Trace.Write(s);
#endif
            this.Database.Log = s => _logger.Info((s.Contains("SELECT") || s.Contains("UPDATE") || s.Contains("DELETE") || s.Contains("INSERT")) ? "\n" + s.Trim() : s.Trim());

        }

        //hack for mvc scaffolding, paramaterles constructor is required
        public DatabaseContext() : this(new UserNameResolver(() => "Anonymous"), NLog.LogManager.GetCurrentClassLogger())
        {

        }

        // regular tables
        //public IDbSet<Person> Persons { get; set; }

        // Identity tables, PK - int
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<UserClaim> UserClaims { get; set; }
        public IDbSet<UserLogin> UserLogins { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<UserRole> UserRoles { get; set; }

        // non-identity tables
        public IDbSet<Event> Events { get; set; }
        public IDbSet<EventMember> EventMembers { get; set; }
        public IDbSet<Course> Courses { get; set; }
        public IDbSet<CourseMember> CourseMembers { get; set; }
        public IDbSet<Album> Albums { get; set; }
        public IDbSet<AlbumPhoto> AlbumPhotos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            // remove tablename pluralizing
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // remove cascade delete
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // Identity, PK - int 
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new UserClaimMap());
            modelBuilder.Configurations.Add(new UserLoginMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserRoleMap());

            modelBuilder.Configurations.Add(new CourseMap());
            modelBuilder.Configurations.Add(new CourseMemberMap());

            // convert all datetime and datetime? properties to datetime2 in ms sql
            // ms sql datetime has min value of 1753-01-01 00:00:00.000
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

            // use Date type in ms sql, where [DataType(DataType.Date)] attribute is used
            modelBuilder.Properties<DateTime>()
           .Where(x => x.GetCustomAttributes(false).OfType<DataTypeAttribute>()
           .Any(a => a.DataType == DataType.Date))
           .Configure(c => c.HasColumnType("date"));

            // relationships
            //modelBuilder.Entity<CourseMember>()
            //    .HasRequired(t => t.Course)
            //    .WithMany(t => t.Members);

            //modelBuilder.Entity<CourseMember>()
            //    .HasRequired(t => t.User)
            //    .WithMany(t => t.Courses);
            //.HasMany(t => t.)
            //.WithMany(t => t.Courses)
            //.Map(m =>
            //{
            //    m.ToTable("CourseInstructor");
            //    m.MapLeftKey("CourseID");
            //    m.MapRightKey("InstructorID");
            //});


        }

        public override int SaveChanges()
        {

            // Update metafields in entitys, that implement IBaseEntity - CreatedAtDT, CreatedBy, etc
            var entities =
                ChangeTracker.Entries()
                .Where(x => x.Entity is IBaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((IBaseEntity)entity.Entity).CreatedAtDT = DateTime.Now;
                    ((IBaseEntity)entity.Entity).CreatedBy = _userNameResolver.CurrentUserName;

                }

                ((IBaseEntity)entity.Entity).ModifiedAtDT = DateTime.Now;
                ((IBaseEntity)entity.Entity).ModifiedBy = _userNameResolver.CurrentUserName;
            }

            // Custom exception - gives much more details why EF Validation failed
            // or watch this inside exception ((System.Data.Entity.Validation.DbEntityValidationException)$exception).EntityValidationErrors
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                throw newException;
            }
        }

        protected override void Dispose(bool disposing)
        {
            _logger.Info("Disposing: " + disposing + " _instanceId: " + _instanceId);
            base.Dispose(disposing);
        }

    }
}
