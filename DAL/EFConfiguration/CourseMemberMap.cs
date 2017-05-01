using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Course;

namespace DAL.EFConfiguration
{
    public class CourseMemberMap : EntityTypeConfiguration<CourseMember>
    {
        public CourseMemberMap()
        {
            // Primary Key
            HasKey(cm => cm.Id);
            //HasKey(t => new { t.UserId, t.CourseId });
        }
    }
}
