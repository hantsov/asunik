using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models.Users;

namespace WebApi.Models.Events
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Heading { get; set; }
        public string Content { get; set; }
        public virtual UserDto Author { get; set; }
    }
}