using Domain.Identity;

namespace Interfaces.Repositories.Identity
{
    public interface IUserRoleRepository : IUserRoleRepository<int, UserRole>
    {
    }

    //public interface IUserRoleRepository : IUserRoleRepository<string, UserRole>
    //{
    //}

    public interface IUserRoleRepository<in TKey, TUserRole> : IBaseRepository<TUserRole>
        where TUserRole : class
    {
        TUserRole GetByUserIdAndRoleId(TKey roleId, TKey userId);
    }
}