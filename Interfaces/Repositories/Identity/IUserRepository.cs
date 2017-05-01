using Domain.Identity;
using Microsoft.AspNet.Identity;

namespace Interfaces.Repositories.Identity
{
    public interface IUserRepository : IUserRepository<int, User>
    {
    }

    public interface IUserRepository<in TKey, TUser> : IBaseRepository<TUser>
        where TUser : class, IUser<TKey>
    {
        TUser GetUserByUserName(string userName);
        TUser GetUserByEmail(string email);
        bool IsInRole(TKey userId, string roleName);
        void AddUserToRole(TKey userId, string roleName);
    }
}