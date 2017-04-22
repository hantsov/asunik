using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models.Users;

namespace WebApi.Models.Courses
{
    public class CoursesCourseMemberDto
    {
        public string MemberRole { get; set; }
        public UserDto User { get; set; }
    }
}