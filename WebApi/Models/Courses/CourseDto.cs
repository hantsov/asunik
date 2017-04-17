using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain.Course;

namespace WebApi.Models.Courses
{
    public class CourseDto
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public string Heading { get; set; }

        [MaxLength(4000)]
        public string Description { get; set; }

        public string Level { get; set; }

        public List<CourseMemberDto> Members { get; set; }
    }
}