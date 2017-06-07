using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Event;
using WebApi.Models.Courses;
using WebApi.Models.Users;

namespace WebApi.Models.Events
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Heading { get; set; }
        public string Content { get; set; }
        public string CreatedAtDT { get; set; }
        public string Type { get; set; }
        public UserDto Author { get; set; }
        public List<EventMemberDto> EventMembers { get; set; }
    }
}