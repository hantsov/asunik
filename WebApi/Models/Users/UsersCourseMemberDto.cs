using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models.Users
{
    public class UsersCourseMemberDto
    {
        public string MemberRole { get; set; }
        public UserCourseDto Course { get; set; }
    }
}