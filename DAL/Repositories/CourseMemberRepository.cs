using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Course;
using Interfaces;
using Interfaces.Repositories;

namespace DAL.Repositories
{
    public class CourseMemberRepository : EFRepository<CourseMember>, ICourseMemberRepository
    {
        public CourseMemberRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public CourseMember GetByCourseAndUserIds(int courseId, int userId)
        {
            return DbSet.FirstOrDefault(cm => cm.CourseId == courseId && cm.UserId == userId);
        }
    }
}
