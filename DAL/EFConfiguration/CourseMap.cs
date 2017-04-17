using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Course;

namespace DAL.EFConfiguration
{
    public class CourseMap : EntityTypeConfiguration<Course>
    {
        public CourseMap()
        {
            HasKey(c => c.Id);
            HasMany(c => c.Members).WithOptional().HasForeignKey(cm => cm.CourseId);
        }
    }
}
