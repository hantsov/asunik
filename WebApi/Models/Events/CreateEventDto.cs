using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models.Users;

namespace WebApi.Models.Events
{
    public class CreateEventDto
    {
        public string Heading { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
    }
}