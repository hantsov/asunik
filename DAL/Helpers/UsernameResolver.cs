using System;
using Interfaces.UOW;

namespace DAL.Helpers
{
    public class UserNameResolver : IUserNameResolver
    {
        private readonly Func<string> _userNameFactory;
        public UserNameResolver(Func<string> userNameFactory)
        {
            _userNameFactory = userNameFactory;
        }

        public string CurrentUserName => _userNameFactory();

    }
}