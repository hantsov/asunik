using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models.Errors
{
    public class ErrorMessages
    {
        public ErrorMessages()
        {
            Errors = new List<string>();
        }

        public List<string> Errors { get; set; }
    }
}