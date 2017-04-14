using System.Collections.Generic;
using Domain.Identity;

namespace Interfaces.Repositories.Identity
{
    public interface IUserLoginRepository : IUserLoginRepository<UserLogin>
    {
    }

    //public interface IUserLoginRepository : IUserLoginRepository<UserLogin>
    //{
    //}

    public interface IUserLoginRepository<TUserLogin> : IBaseRepository<TUserLogin>
        where TUserLogin : class
    {
        List<TUserLogin> GetAllIncludeUser();
        TUserLogin GetUserLoginByProviderAndProviderKey(string loginProvider, string providerKey);
    }
}