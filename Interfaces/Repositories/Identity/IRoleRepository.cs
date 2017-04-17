using System.Collections.Generic;
using Domain.Identity;
using Microsoft.AspNet.Identity;

namespace Interfaces.Repositories.Identity
{
    public interface IRoleRepository : IRoleRepository<int, Role>
    {
    }

    public interface IRoleRepository<in TKey, TRole> : IBaseRepository<TRole>
        where TRole : class, IRole<TKey>
    {
        TRole GetByRoleName(string roleName);
        List<TRole> GetRolesForUser(TKey userId);
    }
}