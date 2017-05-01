using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models.Users
{
    public class UserCourseDto
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public string Heading { get; set; }

        [MaxLength(4000)]
        public string Description { get; set; }

        public string Level { get; set; }

        public string ImgLoc { get; set; }
    }
}