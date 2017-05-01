using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Course;
using Interfaces;
using Interfaces.Repositories;

namespace DAL.Repositories
{
    public class CoursesRepository : EFRepository<Course>, ICourseRepository
    {
        public CoursesRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}