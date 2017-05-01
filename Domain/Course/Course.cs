using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace Domain.Course
{
    public class Course : BaseEntity
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public string Heading { get; set; }

        [MaxLength(4000)]
        public string Description { get; set; }

        public string Level { get; set; }

        [MaxLength(256)]
        public string ImgLoc { get; set; }

        public virtual List<CourseMember> Members { get; set; }

    }
}
