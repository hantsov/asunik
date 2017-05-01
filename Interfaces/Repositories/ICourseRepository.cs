using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Course;

namespace Interfaces.Repositories
{
    public interface ICourseRepository : ICourseBaseRepository<Course>
    {
    }

    public interface ICourseBaseRepository<TCourse> : IBaseRepository<TCourse> where TCourse : class
    {
    }
}
