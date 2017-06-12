using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Identity;

namespace WebApi.Models.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public User Map(User user)
        {
            user.Email = Email;
            user.PhoneNumber = PhoneNumber;
            user.UserName = UserName;
            user.FirstName = FirstName;
            user.LastName = LastName;
            return user;
        }
    }
}