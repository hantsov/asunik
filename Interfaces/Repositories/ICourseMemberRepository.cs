using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Course;

namespace Interfaces.Repositories
{

    public interface ICourseMemberRepository : ICourseMemberRepository<CourseMember>
    {
    }

    public interface ICourseMemberRepository<TCourseMember> : IBaseRepository<TCourseMember> where TCourseMember : class
    {
        TCourseMember GetByCourseAndUserIds(int courseId, int userId);
    }
}
